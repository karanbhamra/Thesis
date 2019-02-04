﻿using System;
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

        public async Task<int> GetStudentRecordCount()
        {
            string query = "SELECT c.id from c";

            Task<int> getRecordCount = Task<int>.Factory.StartNew(() =>
            {
                return client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(PreviousDatabaseName, PreviousTableName), query).ToList().Count;
            });

            int count = await getRecordCount;
            return count;
        }


    }
}
