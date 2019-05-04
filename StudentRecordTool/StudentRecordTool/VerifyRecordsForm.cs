using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReceiveJsonSaveToCosmosFunction;
using Newtonsoft.Json;
namespace StudentRecordTool
{
    public partial class VerifyRecordsForm : Form
    {
        public VerifyRecordsForm()
        {
            InitializeComponent();
        }

        private async void startVerifyButton_Click(object sender, EventArgs e)
        {
            CosmosConnector dbConnector = new CosmosConnector("https://localhost:8081", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");
            dbConnector.PreviousDatabaseName = "StudentDatabase";
            dbConnector.PreviousTableName = "StudentRecords";
            var records = await dbConnector.GetStudentRecords();

            List<FullStudent> fullStudents = new List<FullStudent>();

            foreach (var record in records)
            {
                var dict = (IDictionary<string, object>)record;

                FullStudent fullStudent = new FullStudent()
                {
                    FirstName = dict["FirstName"] as string,
                    MiddleName = dict["MiddleName"] as string,
                    LastName = dict["LastName"] as string,
                    DateOfBirth = (DateTime)dict["DateOfBirth"],
                    Organization = dict["Organization"] as string,
                    SchoolDivision = dict["SchoolDivision"] as string,
                    Degree = dict["Degree"] as string,
                    Awarded = (DateTime)dict["Awarded"],
                    Major = dict["Major"] as string,
                    PreviousRecordHash = dict["PreviousRecordHash"] as string,
                    CurrentNodeHash = dict["CurrentNodeHash"] as string,
                    Salt = dict["Salt"] as string,
                    RecordId = Convert.ToInt32(dict["RecordId"])               
                };

                fullStudents.Add(fullStudent);
            }

            Console.WriteLine($"Total records: {fullStudents.Count}");

            // now that we have the fullstudent records, we can start the verfication of them all


        }
    }
}
