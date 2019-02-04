using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StudentRecordTool.Models;
using System.Net.Http;
using System.Net;
using System.Text;

namespace ReceiveJsonSaveToCosmosFunction
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            BasicStudent studentToAdd = JsonConvert.DeserializeObject<BasicStudent>(requestBody);

            var json = JsonConvert.SerializeObject(studentToAdd, Formatting.Indented);

            var messageToReturn =  new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var failureMessageToReturn = new HttpResponseMessage(HttpStatusCode.BadRequest);

            CosmosConnector dbConnector = new CosmosConnector("https://localhost:8081", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");

            HttpStatusCode databaseCreatedSuccessfulyStatusCode = await dbConnector.CreateDataBase("StudentDatabase");

            HttpStatusCode tableCreatedSuccessfullyStatusCode = await dbConnector.CreateTable(dbConnector.PreviousDatabaseName,"StudentRecords");

            HttpStatusCode recordInsertedStatusCode = dbConnector.InsertStudentRecord(studentToAdd);

            int totalStudents = await dbConnector.GetStudentRecordCount();

            return studentToAdd != null
                ? messageToReturn
                : failureMessageToReturn;
        }
    }
}
