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

            // json is the response that will be returned
            var json = JsonConvert.SerializeObject(student, Formatting.Indented);



            // search the cosmosdb for the record and then grab the record



            // send the record data to the pdf template generator and generate pdf 


            // send pdf back as response




            var successMessageToReturn = new HttpResponseMessage(HttpStatusCode.Accepted)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var failureMessageToReturn = new HttpResponseMessage(HttpStatusCode.NotFound);

            return student.FirstName != null ? successMessageToReturn : failureMessageToReturn;

        }
    }
}
