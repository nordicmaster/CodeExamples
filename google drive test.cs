using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Drive;
using System;
using System.Linq;
using System.Collections.Generic;
//using System.IO;
using System.Threading;
using OfficeOpenXml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp_GoogleDriveTest
{
    class Program
    {
        static string[] Scopes = { DriveService.Scope.Drive};
        static void Main(string[] args)
        {
            UserCredential credential;
            ClientSecrets cs = new ClientSecrets();
            //cs.ClientId = "sdfgsdfgsdfg";
            //cs.ClientSecret = "fsghfsgsfgs"; // from google cloud

            string credPath = "token.json";
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                cs,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "QuickStart"
            });
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var watch2 = System.Diagnostics.Stopwatch.StartNew();
            //string root_folder_id = "1ezjmF31xin3bumvC1arKi4wzlyzuCZyK";
            string root_folder_id = "root";
            List<byte[]> docs = new List<byte[]>();
            List<long> times = new List<long>();

            try
            {
                for (int i = 0; i < 3; i++)
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelPackage _package = new ExcelPackage();
                    ExcelWorksheet oSheet;
                    oSheet = _package.Workbook.Worksheets.Add("Test");

                    oSheet.Cells[2, 1].Value = "TextTextText" + i.ToString();
                    oSheet.Cells[2, 1].Style.Font.Bold = true;
                    docs.Add(_package.GetAsByteArray());
                    _package.Dispose();
                }
                watch2.Stop();
                var elapsedMs2 = watch2.ElapsedMilliseconds;
                Console.WriteLine("time xls = " + elapsedMs2 / 1000 + " sec " + elapsedMs2 % 1000 + "msec");

                for (int i = 0; i < 3; i++)
                {
                    var watchi = System.Diagnostics.Stopwatch.StartNew();
                    createDirectory(service, i.ToString() + " Folder", "Test Folder", root_folder_id);

                    FilesResource.ListRequest listRequest = service.Files.List();
                    listRequest.Q = "name = '" + i.ToString() + " Folder' and trashed = false";
                    listRequest.PageSize = 1000;
                    listRequest.Fields = "nextPageToken, files(id, name)";

                    // List files.
                    IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                        .Files;
                    //Console.WriteLine("Files:");
                    string rootf = "";
                    if (files != null && files.Count > 0)
                    {
                        foreach (var file in files)
                        {
                            //Console.WriteLine("{0} ({1})", file.Name, file.Id);
                            rootf = file.Id;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No files found.");
                    }
                    //Console.Read();
                    Permission perm = new Permission();
                    perm.Type = "user";
                    perm.EmailAddress = "name@gmail.com";
                    perm.Role = "writer";
                    
                    try
                    {
                        service.Permissions.Create(perm, rootf).Execute();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("An error occurred: " + e.Message);
                    }

                    createFile(service, i.ToString() + " Test File.xlsx", "Test Folder", rootf, docs[i]);
                    watchi.Stop();
                    times.Add(watchi.ElapsedMilliseconds);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("time all = " + elapsedMs / 1000 + " sec " + elapsedMs % 1000 + "msec");
            Console.WriteLine("time avg per 1 folder = " + times.Average() / 1000 + " sec");
            Console.ReadKey();
        }
        public static File createDirectory(DriveService _service, string _title, string _description, string _parent)
        {
            var watchi = System.Diagnostics.Stopwatch.StartNew();
            File NewDirectory = null;

            File body = new File();
            body.Name = _title;
            body.Description = _description;
            body.MimeType = "application/vnd.google-apps.folder";
            body.Parents = new List<string>() { _parent };
            try
            {
                FilesResource.CreateRequest request = _service.Files.Create(body);
                NewDirectory = request.Execute();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured1");
            }
            watchi.Stop();
            var elapsedMs = watchi.ElapsedMilliseconds;
            Console.WriteLine("time create dir = " + elapsedMs / 1000 + " sec " + elapsedMs % 1000 + "msec");
            return NewDirectory;
        }

        public static File createFile(DriveService _service, string _title, string _description, string _parent, byte[] bytes)
        {
            var watchi = System.Diagnostics.Stopwatch.StartNew();
            File NewFile = null;
            File body = new File();
            body.Name = _title;
            body.Description = _description;
            body.Parents = new List<string>() { _parent };
            try
            {
                System.IO.MemoryStream stream = new System.IO.MemoryStream(bytes);
                FilesResource.CreateMediaUpload req = _service.Files.Create(body, stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                req.Upload();
                NewFile = req.ResponseBody;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured2");
            }
            var elapsedMs = watchi.ElapsedMilliseconds;
            Console.WriteLine("time create file = " + elapsedMs / 1000 + " sec " + elapsedMs % 1000 + "msec");
            return NewFile;
        }
        public static void deleteFile(DriveService service, String fileId)
        {
            try
            {
                service.Files.Delete(fileId).Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }
    }
}
