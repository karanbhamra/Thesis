using System;
using System.IO;
using DinkToPdf;
using DinkToPdf.Contracts;
using StudentRecordTool.Models;

namespace PdfGenerator
{
    public class Pdf
    {
        private string Title { get; set; }
        private GlobalSettings globalSettings;
        private ObjectSettings objectSettings;

        private HtmlToPdfDocument pdfDocument;

        private IConverter Converter { get; set; }

        private string htmlData;
        
        public Pdf(string title, string data)
        {
            Title = title;  
            Converter = new SynchronizedConverter(new PdfTools());
            htmlData = data;
            setDocumentSettings();
            
        }

        public Pdf(BasicStudent student)
        {
            Title = $"{student.FirstName} {student.MiddleName} {student.LastName} Academic Records";
            Converter = new SynchronizedConverter(new PdfTools());
            htmlData = TemplateGenerator.GetHTMLString(student);

            setDocumentSettings();
        }

        private void setDocumentSettings()
        {
            
            globalSettings = new GlobalSettings()
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = Title,
            };

            objectSettings = new ObjectSettings()
            {
                PagesCount = true,
                HtmlContent = htmlData,//TemplateGenerator.GetHTMLString(),    // HTML String Goes here for the data
                // PDF stylesheet is saved into PdfGenerator/assets/styles.css
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet =  Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "CSUN Student Report" }

            };

            pdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = {objectSettings}
            };

        }


        public byte[] SaveToByteArray()
        {
            return Converter.Convert(pdfDocument);
        }

        public string SaveToBase64String()
        {
            byte[] input = SaveToByteArray();

            string output = Convert.ToBase64String(input);

            return output;
        }

        public bool SaveToFile(string path)
        {
            byte[] fileBytes = SaveToByteArray();

            try
            {
                File.WriteAllBytes(path, fileBytes);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
        }
    }
}