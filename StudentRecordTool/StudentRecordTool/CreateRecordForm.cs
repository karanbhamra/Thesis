using Newtonsoft.Json;
using ReceiveJsonSaveToCosmosFunction;
using StudentRecordTool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UtilityFunctions;

namespace StudentRecordTool
{
    public partial class CreateRecordForm : Form
    {
        Form1 mainForm;

        public CreateRecordForm(Form1 main)
        {
            InitializeComponent();

            mainForm = main;
        }

        private void CreateRecordForm_Load(object sender, EventArgs e)
        {
            organization.Text = "California State University - Northridge";
            schoolDivision.Text = "College of Eng/ Comp Sci";
            degree.Text = "Bachelor of Science";
            major.Text = "Computer Science";
            dateOfBirthPicker.Value = StudentMapper.UnixEpoch;
            dateAwarded.Value = StudentMapper.UnixEpoch;
        }

        private void CreateRecordForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.Show();
        }

        private void generateRecord_Click(object sender, EventArgs e)
        {
            if (firstName.Text.Length == 0 || middleName.Text.Length == 0 || lastName.Text.Length == 0)
            {
                MessageBox.Show("Enter the text in the missing fields please");
                return;
            }

            BasicStudent tempStudent = new BasicStudent()
            {
                FirstName = firstName.Text.CapitalizeEveryLetterOnSplit(' '),
                MiddleName = middleName.Text.CapitalizeEveryLetterOnSplit(' '),
                LastName = lastName.Text.CapitalizeEveryLetterOnSplit(' '),
                DateOfBirth = dateOfBirthPicker.Value,
                Organization = organization.Text,
                SchoolDivision = schoolDivision.Text,
                Degree = degree.Text,
                Awarded = dateAwarded.Value,
                Major = major.Text
            };

            // serialize JSON directly to a file

            string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);


            string filename = $"{tempStudent.FirstName}{tempStudent.MiddleName}{tempStudent.LastName}.json";

           
            using (StreamWriter file = File.CreateText($@"{directoryName}/{filename}"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, tempStudent);

                MessageBox.Show("File generated");

                this.Close();
            }
            

        }
    }
}
