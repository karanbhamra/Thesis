using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace PdfGenerator
{
    
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "output", "test.pdf");

//            Console.WriteLine(path);
            Console.WriteLine(path);
            
            IConverter converter = new SynchronizedConverter(new PdfTools());
            
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
                Out = path//$"{Directory.GetCurrentDirectory() + "\test.pdf"}"   //@"D:\PDFCreator\Employee_Report.pdf"
            };
 
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetHTMLString(),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet =  Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };
 
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            converter.Convert(pdf);
            
            Console.WriteLine("Converted pdf successfully.");
            //*/
        }
    }
}