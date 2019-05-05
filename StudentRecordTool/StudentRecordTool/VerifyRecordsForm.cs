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
            progressBar1.Value = 50;

            Console.WriteLine($"Total records: {students.Count}");

            string student1 = "";
            string student2 = "";

 
            numOfRecords.Text = students.Count.ToString();

            // now that we have the fullstudent records, we can start the verfication of them all
            BasicStudent genesisStudent = StudentMapper.GenesisStudentNode();

            bool valid = true;
            for (int i = students.Count -1 ; i >= 0; i--)
            {
                bool currentHashMatch = true;
                bool previousHashMatch = true;

                FullStudent currentFullStudent = students[i];

                Console.WriteLine($"Current Student: {currentFullStudent.FirstName}");
                string recalculatedCurrentNodeHash = CalculateCurrentFullStudentHash(currentFullStudent);

                if (recalculatedCurrentNodeHash != currentFullStudent.CurrentNodeHash)
                {
                    currentHashMatch = false;
                }

                if (i == 0)
                {
                    Console.WriteLine($"Genesis Node Previous: Test");
                    string previousGenesisHash = Hash.GetHashString("Test");

                    if (currentFullStudent.PreviousRecordHash != previousGenesisHash)
                    {
                        previousHashMatch = false;
                    }

                }
                else
                {

                    FullStudent previousFullStudent = students[i - 1];
                    Console.WriteLine($"Previous Student: {previousFullStudent.FirstName}");
                    string recalculatedPreviousNodeHash = CalculateCurrentFullStudentHash(previousFullStudent);

                    if (currentFullStudent.PreviousRecordHash != recalculatedPreviousNodeHash)
                    {
                        previousHashMatch = false;
                        student2 = previousFullStudent.FirstName;
                    }
                }

                if (!currentHashMatch || !previousHashMatch)
                {
                    valid = false;
                    student1 = currentFullStudent.FirstName;
                    break;
                }

            }
            progressBar1.Value = 100;

            if (valid)
            {

                MessageBox.Show("Hash Verified and Unmodified", "Success");
            }
            else
            {
                MessageBox.Show($"Hash mismatch on {student1} and {student2}", "Error");

            }

        }

        string CalculateCurrentFullStudentHash(FullStudent currentFullStudent)
        {
            BasicStudent currentBasicStudent = StudentMapper.FullStudentToBasicStudent(currentFullStudent);

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
