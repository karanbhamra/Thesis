using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SHA512HashGenerator;

namespace StudentRecordTool
{
    public partial class HashUtilityForm : Form
    {
        public HashUtilityForm()
        {
            InitializeComponent();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            string input = inputTextBox.Text;

            if (input.Length == 0)
            {
                MessageBox.Show("Input cannot be empty.");
                return;
            }

            string output = "";
            int workfactor = (int)bcryptNumeric.Value;

            if (shaRadioButton.Checked == true)
            {
                output = Hash.GetHashString(input);
            }
            else
            {
                output = Hash.GetBCryptHashAutoSalt(input, workfactor);
            }

            outputTextbox.Text = output;
        }
    }
}
