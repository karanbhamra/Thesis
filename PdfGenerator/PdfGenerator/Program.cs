using System;

namespace PdfGenerator
{
    
    class Program
    {
        static void Main(string[] args)
        {
            
            Pdf mydoc = new Pdf("CSUN Student Report", TemplateGenerator.GetHTMLString());

            var result = mydoc.SaveToFile("testfile.pdf");

            if (result)
            {
                Console.WriteLine("Converted pdf successfully.");
            }
            else
            {
                Console.WriteLine("Failed to save pdf");
            }

        }
    }
}