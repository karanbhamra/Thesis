using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using ReceiveJsonSaveToCosmosFunction;
using StudentRecordTool.Models;
using System.Collections.Generic;
using PdfGenerator;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.Extensions.Configuration;
using UtilityFunctions;

namespace ReceiveStudentNameAndSendEncodedPdfRecord
{
    public static class Function1
    {
        static int count = 0;
        // Needs a single instance of the converter, dies on second request otherwise
        static IConverter converter = new SynchronizedConverter(new PdfTools());

        [FunctionName("GetPdf")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            log.LogInformation($"C# HTTP trigger function processed request #{++count}.");

            // load the connection string for the db
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            Student student = JsonConvert.DeserializeObject<Student>(requestBody);
            student.FirstName = student.FirstName.CapitalizeEveryLetterOnSplit(' ');
            student.MiddleName = student.MiddleName.CapitalizeEveryLetterOnSplit(' ');
            student.LastName = student.LastName.CapitalizeEveryLetterOnSplit(' ');


            // search the cosmosdb for the record and then grab the record
            dynamic studentCosmosRecord;

            string uri = config.GetConnectionString("address");
            string accessKey = config.GetConnectionString("accessKey");
            using (CosmosConnector dbConnector = new CosmosConnector(uri, accessKey))
            {
                dbConnector.UseDataBase("StudentDatabase");
                dbConnector.UseTableName("StudentRecords");
                studentCosmosRecord = dbConnector.GetFullStudentRecordFromName(student);
            }

            dynamic previousRecord = studentCosmosRecord;

            var dict = (IDictionary<string, object>)previousRecord;

            BasicStudent basicStudent = new BasicStudent()
            {
                FirstName = dict["FirstName"] as string,
                MiddleName = dict["MiddleName"] as string,
                LastName = dict["LastName"] as string,
                DateOfBirth = (DateTime)dict["DateOfBirth"],
                Organization = dict["Organization"] as string,
                SchoolDivision = dict["SchoolDivision"] as string,
                Degree = dict["Degree"] as string,
                Awarded = (DateTime)dict["Awarded"],
                Major = dict["Major"] as string
            };


            // send the record data to the pdf template generator and generate pdf 
            Pdf studentPdf = new Pdf(basicStudent, converter);
            string pdfOutput = studentPdf.SaveToBase64String();

            // send pdf back as response

            object dataToSendBack = new
            {
                Pdf = pdfOutput
            };

            var json = JsonConvert.SerializeObject(dataToSendBack, Formatting.Indented);

            var successMessageToReturn = new HttpResponseMessage(HttpStatusCode.Accepted)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var failureMessageToReturn = new HttpResponseMessage(HttpStatusCode.NotFound);

            return student.FirstName != null ? successMessageToReturn : failureMessageToReturn;

        }
    }
}
