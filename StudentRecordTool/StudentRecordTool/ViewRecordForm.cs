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

namespace StudentRecordTool
{
    public partial class ViewRecordForm : Form
    {
        string filename;
        BasicStudent basicStudent;
        public ViewRecordForm(string file)
        {
            InitializeComponent();

            filename = file;

            LoadJson(filename);

            LoadFormFields();
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
