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
using StudentRecordTool.Models;
 using Newtonsoft.Json;
namespace StudentRecordTool
{
    public partial class GetRecordForm : Form
    {
        public GetRecordForm()
        {
            InitializeComponent();
        }

        private async void retrieveStudentButton_Click(object sender, EventArgs e)
        {
            if (firstName.Text.Length == 0 || middleName.Text.Length == 0 || lastName.Text.Length == 0)
            {
                MessageBox.Show("The name fields cannot be empty!");
                return;
            }

            List<FullStudent> students = await GetAllFullStudents();

            Func<FullStudent, bool> predicate = x => x.FirstName == firstName.Text && x.LastName == lastName.Text && x.MiddleName == middleName.Text;

            try
            {
                FullStudent fullStudent = students.First(predicate);

                if (fullStudent != null)
                {
                    string fullStudentJSON = JsonConvert.SerializeObject(fullStudent);
                    BasicStudent basicStudent = StudentMapper.FullStudentToBasicStudent(fullStudent);
                    string basicStudentJSON = JsonConvert.SerializeObject(basicStudent);

                    basicStudentTextBox.Text = basicStudentJSON;
                    fullStudentTextBox.Text = fullStudentJSON;
                }
            } catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        private async Task<List<FullStudent>> GetAllFullStudents()
        {

            var url = UtilityFunctions.UtilityFunctions.GetValueOfSetting("cosmosUrl");
            var accessKey = UtilityFunctions.UtilityFunctions.GetValueOfSetting("cosmosAccessKey");
            IDatabaseConnector dbConnector = new CosmosConnector(url, accessKey);
            dbConnector.PreviousDatabaseName = "StudentDatabase";
            dbConnector.PreviousTableName = "StudentRecords";
            var records = await dbConnector.GetStudentRecords();

            List<FullStudent> fullStudents = new List<FullStudent>();

            foreach (var record in records)
            {
                var dict = (IDictionary<string, object>)record;

                FullStudent fullStudent = StudentMapper.DictionaryObjectToFullStudent(dict);

                fullStudents.Add(fullStudent);
            }

            return fullStudents;
        }
    }
}
