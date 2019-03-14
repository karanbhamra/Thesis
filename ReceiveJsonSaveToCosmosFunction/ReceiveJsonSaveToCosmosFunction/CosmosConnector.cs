using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;
using StudentRecordTool.Models;
using System.Threading.Tasks;
using System.Net;
using System.Linq;

namespace ReceiveJsonSaveToCosmosFunction
{
    class CosmosConnector
    {
        public string Uri { get; set; }
        public string AccessKey { get; set; }
        public DocumentClient client { get; set; }

        public string PreviousDatabaseName { get; set; }
        public string PreviousTableName { get; set; }

        public CosmosConnector(string uri, string accessKey)
        {
            this.Uri = uri;
            AccessKey = accessKey;

            client = new DocumentClient(new Uri(Uri), AccessKey);
        }

        public async Task<HttpStatusCode> CreateDataBase(string databaseName)
        {
            PreviousDatabaseName = databaseName;

            var db = await client.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseName });

            var statusCode = db.StatusCode;

            return statusCode;
        }

        public async Task<HttpStatusCode> CreateTable(string databaseName, string tableName)
        {
            PreviousTableName = tableName;
            var collectionResult = await this.client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(databaseName), new DocumentCollection { Id = tableName });

            var statusCode = collectionResult.StatusCode;

            return statusCode;
        }

        public HttpStatusCode InsertStudentRecord(BasicStudent student)
        {
            var response = this.CreateStudentocumentIfNotExists(PreviousDatabaseName, PreviousTableName, student).GetAwaiter().GetResult();

            return response;
        }

        private async Task<HttpStatusCode> CreateStudentocumentIfNotExists(string databaseName, string collectionName, BasicStudent student)
        {
            var response = await this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), student);

            var statusCode = response.StatusCode;

            return statusCode;

        }

        public async Task<List<dynamic>> GetLastAddedStudentRecordByStudentId()
        {
            // Query should be with the student id
            string query = "SELECT top 1 * from c order by c._StudentId";
            Task<List<dynamic>> studentRecord = Task<List<dynamic>>.Factory.StartNew(() =>
            {
                var list = client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(PreviousDatabaseName, PreviousTableName), query).ToList();
                return list;
            });

            return await studentRecord;
        }

        public async Task<List<dynamic>> GetStudentRecords()
        {
            string query = "SELECT * from c order by c.RecordId";

            Task<List<dynamic>> getRecords = Task<List<dynamic>>.Factory.StartNew(() =>
            {
                var list = client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(PreviousDatabaseName, PreviousTableName), query).ToList();
                return list;
            });

            return await getRecords;

           // int count = await getRecordCount;
           // return count;
        }

        public async Task<int> GetStudentRecordCount()
        {
            var records =  await GetStudentRecords();

            int count = records.Count;

            return count;


        }


    }
}
