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

namespace ReceiveStudentNameAndSendEncodedPdfRecord
{
    public static class Function1
    {
        [FunctionName("GetPdf")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            Student student = JsonConvert.DeserializeObject<Student>(requestBody);

            // search the cosmosdb for the record and then grab the record

            CosmosConnector dbConnector = new CosmosConnector("https://localhost:8081", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");

            dbConnector.UseDataBase("StudentDatabase");
            dbConnector.UseTableName("StudentRecords");
            var studentCosmosRecord = await dbConnector.GetFullStudentRecordFromName(student);


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
            Pdf studentPdf = new Pdf(basicStudent);

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
