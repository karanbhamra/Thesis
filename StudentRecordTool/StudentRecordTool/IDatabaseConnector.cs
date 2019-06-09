using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using StudentRecordTool.Models;

namespace ReceiveJsonSaveToCosmosFunction
{
    interface IDatabaseConnector
    {
        string AccessKey { get; set; }
        DocumentClient client { get; set; }
        string PreviousDatabaseName { get; set; }
        string PreviousTableName { get; set; }
        string Uri { get; set; }

        Task<HttpStatusCode> CreateDataBase(string databaseName);
        Task<HttpStatusCode> CreateTable(string databaseName, string tableName);
        void Dispose();
        dynamic GetFullStudentRecordFromName(BasicStudent student);
        Task<System.Collections.Generic.List<dynamic>> GetLastAddedStudentRecordByStudentId();
        Task<int> GetStudentRecordCount();
        Task<System.Collections.Generic.List<dynamic>> GetStudentRecords();
        HttpStatusCode InsertStudentRecord(BasicStudent student);
        HttpStatusCode InsertStudentRecord(FullStudent student);
    }
}