using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using Microsoft.Win32;
using System.IO;
using System.Net;

namespace ConsoleApp_NextCloudTest
{

    class Program
    {
        public static string Get(string uri)
        {
            string AccessToken = "1vf17svbllh4i2snk8l";
            String username = "test-user02";
            String password = "test-user02";
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.PreAuthenticate = true;
            request.Headers.Add("Authorization", "Basic " + encoded);
            Encoding encoding = Encoding.GetEncoding("windows-1251");

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream,encoding))
            {
                var ss = reader.ReadToEnd();
                return ss;
            }
        }


        static void Main()
        {
            string test = "http://192.168.31.20/remote.php/dav/files/test-user02/New%20Spreadsheet.ods";
            string res = Get(test);
            Console.WriteLine(res);
            var fs = File.Create("12.txt");
            Encoding encoding = Encoding.GetEncoding("windows-1251");
            byte[] bytes = encoding.GetBytes(res);
            fs.Write(bytes,0,bytes.Length);
            fs.Close();
            Console.ReadKey();
        }
    }
}
