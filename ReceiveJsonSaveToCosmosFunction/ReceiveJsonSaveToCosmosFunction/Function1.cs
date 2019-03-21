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
using SHA512HashGenerator;
using System.Collections.Generic;

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

            // When returning, lets return the full student so that it may be tested further
            var json = JsonConvert.SerializeObject(studentToAdd, Formatting.Indented);

            var successMessageToReturn = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var failureMessageToReturn = new HttpResponseMessage(HttpStatusCode.NotFound);

            CosmosConnector dbConnector = new CosmosConnector("https://localhost:8081", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");

            HttpStatusCode databaseCreatedSuccessfulyStatusCode = await dbConnector.CreateDataBase("StudentDatabase");

            HttpStatusCode tableCreatedSuccessfullyStatusCode = await dbConnector.CreateTable(dbConnector.PreviousDatabaseName, "StudentRecords");

            // last record holds the last inserted record
            var lastRecord = await dbConnector.GetLastAddedStudentRecordByStudentId();

            FullStudent fullStudent;

            bool addingForFirstTime = false;

            // if no record was retrieved (empty database, insert genesis block)
            if (lastRecord.Count == 0)
            {
                // no record returned, add the genesis block
                // generate previous node hash for genesis student
                // generate genesis student hash, and salthash and combined hash

                BasicStudent genesisStudent = StudentMapper.GenesisStudentNode();

                string hashForGenesisStudentPrevious = Hash.GetHashString("Test");

                byte[] genesisStudentBytes = ObjectHasher.GetBytes(genesisStudent);

                string genesisStudentHash = Hash.GetHashBytesAsString(genesisStudentBytes);
                string[] genesissaltAndSaltHashArray = Hash.GetRandomSaltWithHash();

                string genesissalt = genesissaltAndSaltHashArray[0];
                string genesissaltHash = genesissaltAndSaltHashArray[1];

                string genesisstudentHashPlusSaltHash = Hash.GetHashString(genesisStudentHash + genesissaltHash);

                fullStudent = StudentMapper.Map(genesisStudent, hashForGenesisStudentPrevious, genesisstudentHashPlusSaltHash, genesissalt);

                HttpStatusCode genesisrecordInsertedStatusCode = dbConnector.InsertStudentRecord(fullStudent);

                addingForFirstTime = true;

            }

            if (addingForFirstTime)
            {
                lastRecord = await dbConnector.GetLastAddedStudentRecordByStudentId();
            }

            byte[] studentToAddBytes = ObjectHasher.GetBytes(studentToAdd);

            string studentToAddHash = Hash.GetHashBytesAsString(studentToAddBytes);

            string[] saltAndSaltHashArray = Hash.GetRandomSaltWithHash();

            string salt = saltAndSaltHashArray[0];
            string saltHash = saltAndSaltHashArray[1];

            string studentHashPlusSaltHash = Hash.GetHashString(studentToAddHash + saltHash);


            dynamic previousRecord = lastRecord[0];

            var dict = (IDictionary<string, object>)previousRecord;
            string previousRecordHash = dict["CurrentNodeHash"] as string;
            int previousRecordId = Convert.ToInt32(dict["RecordId"]);

            fullStudent = StudentMapper.Map(studentToAdd, previousRecordHash, studentHashPlusSaltHash, salt, previousRecordId + 1);

            HttpStatusCode recordInsertedStatusCode = dbConnector.InsertStudentRecord(fullStudent);



            //var studentRecords = await dbConnector.GetStudentRecords();

            //int totalStudents = await dbConnector.GetStudentRecordCount();

            return studentToAdd != null
                ? successMessageToReturn
                : failureMessageToReturn;
        }
    }
}
