    public static String convertPdfToText(String inStr) throws Exception{
        String res = "";
        System.out.println("))))))))))))))))))88");
        //System.out.println(new String(com.itextpdf.text.pdf.parser.PdfTextExtractor.getTextFromPage(new PdfReader("C:\\Users\\dryabuhin\\Desktop\\rdve.pdf"),1)));
        PDDocument document = PDDocument.load(inStr.getBytes());

        //Instantiate PDFTextStripper class
        PDFTextStripper pdfStripper = new PDFTextStripper();

        //Retrieving text from PDF document
        String text = pdfStripper.getText(document);
        System.out.println(text);
        System.out.println(")))))))))))))))))))77");
        inStr = inStr.substring(45);//exclude first "Exstream" word
        String[] strs = StringUtils.substringsBetween(inStr,"stream","endstream");


        int i = 0;
        for (String s: strs) {
            i++;
            System.out.println(")))))))))))))))))))begin "+i);
            s=s.trim();
            System.out.println(s);
            System.out.println(")))))))))))))))))))1 "+i);
            byte[] b = s.getBytes();
            byte[] b2 = PdfReader.ASCII85Decode(b);
            System.out.println(new String(b2, StandardCharsets.UTF_8));
            System.out.println(")))))))))))))))))))2 "+i);
            byte[] b3 = PdfReader.FlateDecode(b2);
            System.out.println(new String(b3, StandardCharsets.UTF_8));
            System.out.println(")))))))))))))))))))end "+i);
        }
        return res;
    }