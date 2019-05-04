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

namespace StudentRecordTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void createRecord_Click(object sender, EventArgs e)
        {
            this.Hide();

            CreateRecordForm recordForm = new CreateRecordForm(this);

            recordForm.Show();
        }

        private void viewRecord_Click(object sender, EventArgs e)
        {
            setupRecordForm("ViewRecordForm", false);

        }

        private void verifyRecords_Click(object sender, EventArgs e)
        {
            // TODO: verify the records

        }

        private void addRecords_Click(object sender, EventArgs e)
        {
            //// Add the generated json file to the collection in cosmosdb

            setupRecordForm("AddRecordForm", true);
        }

        public void setupRecordForm(string formTitle, bool isAddForm)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            fileDialog.Filter = "JSON (*.json) | *.json";
            fileDialog.InitialDirectory = directoryName;
            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string file = fileDialog.FileName;
                ViewRecordForm viewRecordForm = new ViewRecordForm(file, formTitle, isAddForm);
                viewRecordForm.Show();
            }
        }
    }
}
