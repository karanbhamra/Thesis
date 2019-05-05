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
using StudentRecordTool.Models;
using SHA512HashGenerator;

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
            progressBar1.Value = 25;

            List<FullStudent> students = await GetAllFullStudents();

            Console.WriteLine($"Total records: {students.Count}");

            // now that we have the fullstudent records, we can start the verfication of them all
            BasicStudent genesisStudent = StudentMapper.GenesisStudentNode();
            //string previousHash = Hash.GetHashString("Test");   // start with the hash of previous which is C6EE9....

            // This works, now lets try from reverse
            //foreach (var currentStudent in students)
            //{
            //    bool matched = true;
            //    if (currentStudent.PreviousRecordHash != previousHash)
            //    {
            //        matched = false;
            //    }

            //    if (!matched)
            //    {
            //        MessageBox.Show("Hash MisMatched!");
            //        return;
            //    }

            //    previousHash = currentStudent.CurrentNodeHash;

            //}

            bool valid = true;
            for (int i = students.Count -1 ; i >= 0; i--)
            {
                FullStudent currentFullStudent = students[i];

                // think about how to deal with previous hash now that the current hash has been verified

                if (i ==0)
                {
                    string previousGenesisHash = Hash.GetHashString("Test");


                }

                string recalculatedCurrentNodeHash = CalculateCurrentFullStudentHash(currentFullStudent);

                if (recalculatedCurrentNodeHash != currentFullStudent.CurrentNodeHash)
                {
                    valid = false;
                }

                ; 

            }

            if (valid)
            {

                MessageBox.Show("Hash Verified and Unmodified");
            }
            else
            {
                MessageBox.Show("Hash mismatch.");

            }




        }

        string CalculateCurrentFullStudentHash(FullStudent currentFullStudent)
        {
            BasicStudent currentBasicStudent = StudentMapper.FullStudentToBasicStudent(currentFullStudent);

            //var currentBasicBytes = ObjectHasher.GetBytes(currentBasicStudent);
            //var currentBasicHash = Hash.GetHashBytesAsString(currentBasicBytes);

            string currentBasic = JsonConvert.SerializeObject(currentBasicStudent);
            string currentBasicHash = Hash.GetHashString(currentBasic);

            string currentSalt = currentFullStudent.Salt;
            string currentSaltHash = Hash.GetHashString(currentSalt);

            string fullHash = Hash.GetHashString(currentBasicHash + currentSaltHash);

            return fullHash;

        }

        private async Task<List<FullStudent>> GetAllFullStudents()
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

            return fullStudents;
        }
    }
}
