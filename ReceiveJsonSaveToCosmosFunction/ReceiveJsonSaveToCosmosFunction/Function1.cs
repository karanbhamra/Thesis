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
        [FunctionName("SaveStudent")]
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

            var url = Environment.GetEnvironmentVariable("cosmosUrl");
            var accessKey = Environment.GetEnvironmentVariable("cosmosAccesskey");
            IDatabaseConnector dbConnector = new CosmosConnector(url, accessKey);

            HttpStatusCode databaseCreatedSuccessfulyStatusCode = await dbConnector.CreateDataBase("StudentDatabase");

            HttpStatusCode tableCreatedSuccessfullyStatusCode = await dbConnector.CreateTable(dbConnector.PreviousDatabaseName, "StudentRecords");

            // last record holds the last inserted record
            var lastRecord = await dbConnector.GetLastAddedStudentRecordByStudentId();

            FullStudent fullStudentToAdd = null;

            bool addingForFirstTime = false;

            // if no record was retrieved (empty database, insert genesis block)
            if (lastRecord.Count == 0)
            {

                InsertGenesisStudent(fullStudentToAdd, dbConnector);

                addingForFirstTime = true;

            }

            if (addingForFirstTime)
            {
                // If we are adding for the first time, lets get the record of the last student added from the database
                lastRecord = await dbConnector.GetLastAddedStudentRecordByStudentId();
            }

            string studentToAddSerialized = JsonConvert.SerializeObject(studentToAdd);
            string studentToAddHash = Hash.GetHashString(studentToAddSerialized);

            string[] saltAndSaltHashArray = Hash.GetRandomSaltWithHash();

            string salt = saltAndSaltHashArray[0];
            string saltHash = saltAndSaltHashArray[1];

            string studentHashPlusSaltHash = Hash.GetHashString(studentToAddHash + saltHash);


            dynamic previousRecord = lastRecord[0];

            var dict = (IDictionary<string, object>)previousRecord;
            string previousRecordHash = dict["CurrentNodeHash"] as string;
            int previousRecordId = Convert.ToInt32(dict["RecordId"]);

            fullStudentToAdd = StudentMapper.Map(studentToAdd, previousRecordHash, studentHashPlusSaltHash, salt, previousRecordId + 1);


            //string previousstudentfullhash = dict["PreviousFullRecordHash"] as string;

            // calculate out the full record hash of the previous full student

            FullStudent previousFullStudent = StudentMapper.DictionaryObjectToFullStudent(dict);


            string previousFullStudentSerialized = JsonConvert.SerializeObject(previousFullStudent);

            Console.WriteLine(previousFullStudentSerialized);

            string previousFullBCryptHash = Hash.GetHashString(previousFullStudentSerialized);//Hash.GetBCryptHashAutoSalt(previousFullStudentSerialized);

            // save the full hash of the previous student
            fullStudentToAdd.PreviousFullRecordHash = previousFullBCryptHash;

            HttpStatusCode recordInsertedStatusCode = dbConnector.InsertStudentRecord(fullStudentToAdd);

            if (recordInsertedStatusCode == HttpStatusCode.Created)
            {
                return successMessageToReturn;
            }
            else
            {
                return failureMessageToReturn;
            }
        }

        private static void InsertGenesisStudent(FullStudent fullStudent, IDatabaseConnector dbConnector)
        {
            // no record returned, add the genesis block
            // generate previous node hash for genesis student
            // generate genesis student hash, and salthash and combined hash

            BasicStudent genesisStudent = StudentMapper.GenesisStudentNode();

            // SHA512 hash of 512 for the previous for genesisStudent
            string hashForGenesisStudentPrevious = Hash.GetHashString("Test");// Hash.GetBCryptHashAutoSalt("Test");

            string genesisStudentSerialized = JsonConvert.SerializeObject(genesisStudent);
            string genesisStudentHash = Hash.GetHashString(genesisStudentSerialized);

            Console.WriteLine($"Genesis studnet");
            Console.WriteLine(genesisStudentSerialized);

            string[] genesissaltAndSaltHashArray = Hash.GetRandomSaltWithHash();

            string genesissalt = genesissaltAndSaltHashArray[0];
            string genesissaltHash = genesissaltAndSaltHashArray[1];

            string genesisstudentHashPlusSaltHash = Hash.GetHashString(genesisStudentHash + genesissaltHash);

            fullStudent = StudentMapper.Map(genesisStudent, hashForGenesisStudentPrevious, genesisstudentHashPlusSaltHash, genesissalt);

            // add the full hash of the previous student

            fullStudent.PreviousFullRecordHash = hashForGenesisStudentPrevious;

            HttpStatusCode genesisrecordInsertedStatusCode = dbConnector.InsertStudentRecord(fullStudent);

        }
    }
}
