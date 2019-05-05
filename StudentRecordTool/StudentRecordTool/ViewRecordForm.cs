using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudentRecordTool.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace StudentRecordTool
{
    public partial class ViewRecordForm : Form
    {
        string filename;
        BasicStudent basicStudent;

        private static readonly HttpClient client = new HttpClient();

        public ViewRecordForm(string file, string title, bool isAddForm)
        {
            InitializeComponent();

            filename = file;

            this.Text = title;

            if (isAddForm)
            {
                this.addRecordButton.Visible = true;
                this.addRecordButton.Click += AddRecordButton_Click;
            }

            foreach (Control control in Controls)
            {
                if (control.Text != "Add")
                {
                    control.Enabled = false;
                }
            }

            LoadJson(filename);

            LoadFormFields();
        }

        private void AddRecordButton_Click(object sender, EventArgs e)
        {
            // call the function which takes the json and adds it to the cosmosdb
            if (UtilityFunctions.UtilityFunctions.IsLocalEnvironment())
            {
                string url = UtilityFunctions.UtilityFunctions.GetValueOfSetting("devUrl");

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(basicStudent);
                    streamWriter.Write(json);
                }
                httpWebRequest.Timeout = int.MaxValue;
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                if (httpResponse.StatusCode == HttpStatusCode.Created)
                {
                    MessageBox.Show($"Successfully added {basicStudent.FirstName}");
                }
                else if (httpResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    MessageBox.Show($"Error adding {basicStudent.FirstName}");
                }
                else
                {
                    MessageBox.Show($"Something went really wrong adding ${basicStudent.FirstName}, the returned status code was wrong");
                }
            }

        }

        private void LoadFormFields()
        {
            firstName.Text = basicStudent.FirstName;
            middleName.Text = basicStudent.MiddleName;
            lastName.Text = basicStudent.LastName;
            dateOfBirthPicker.Value = basicStudent.DateOfBirth;
            organization.Text = basicStudent.Organization;
            degree.Text = basicStudent.Degree;
            schoolDivision.Text = basicStudent.SchoolDivision;
            dateAwarded.Value = basicStudent.Awarded;
            major.Text = basicStudent.Major;

        }

        private void LoadJson(string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                string json = reader.ReadToEnd();

                basicStudent = JsonConvert.DeserializeObject<BasicStudent>(json);
            }
        }

        private void ViewRecordForm_Load(object sender, EventArgs e)
        {

        }
    }
}
