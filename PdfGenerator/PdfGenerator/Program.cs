using System;

namespace PdfGenerator
{
    
    class Program
    {
        static void Main(string[] args)
        {
            
            Pdf mydoc = new Pdf("CSUN Student Report", TemplateGenerator.GetHTMLString());

//            var result = mydoc.SaveToFile("testfile.pdf");

            string output = mydoc.SaveToBase64String();

            Console.WriteLine(output);
            
        


        }
    }
}