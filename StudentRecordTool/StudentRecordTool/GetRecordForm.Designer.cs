namespace StudentRecordTool
{
    partial class GetRecordForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lastName = new System.Windows.Forms.TextBox();
            this.middleName = new System.Windows.Forms.TextBox();
            this.firstName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.retrieveStudentButton = new System.Windows.Forms.Button();
            this.basicStudentTextBox = new System.Windows.Forms.TextBox();
            this.fullStudentTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lastName
            // 
            this.lastName.Location = new System.Drawing.Point(659, 165);
            this.lastName.Name = "lastName";
            this.lastName.Size = new System.Drawing.Size(384, 31);
            this.lastName.TabIndex = 11;
            // 
            // middleName
            // 
            this.middleName.Location = new System.Drawing.Point(659, 99);
            this.middleName.Name = "middleName";
            this.middleName.Size = new System.Drawing.Size(384, 31);
            this.middleName.TabIndex = 10;
            // 
            // firstName
            // 
            this.firstName.Location = new System.Drawing.Point(659, 35);
            this.firstName.Name = "firstName";
            this.firstName.Size = new System.Drawing.Size(384, 31);
            this.firstName.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(443, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "Last Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(443, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Middle name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(443, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "First Name";
            // 
            // retrieveStudentButton
            // 
            this.retrieveStudentButton.Location = new System.Drawing.Point(739, 228);
            this.retrieveStudentButton.Name = "retrieveStudentButton";
            this.retrieveStudentButton.Size = new System.Drawing.Size(215, 79);
            this.retrieveStudentButton.TabIndex = 12;
            this.retrieveStudentButton.Text = "Retrieve";
            this.retrieveStudentButton.UseVisualStyleBackColor = true;
            this.retrieveStudentButton.Click += new System.EventHandler(this.retrieveStudentButton_Click);
            // 
            // basicStudentTextBox
            // 
            this.basicStudentTextBox.Location = new System.Drawing.Point(43, 440);
            this.basicStudentTextBox.Multiline = true;
            this.basicStudentTextBox.Name = "basicStudentTextBox";
            this.basicStudentTextBox.Size = new System.Drawing.Size(730, 406);
            this.basicStudentTextBox.TabIndex = 15;
            // 
            // fullStudentTextBox
            // 
            this.fullStudentTextBox.Location = new System.Drawing.Point(887, 440);
            this.fullStudentTextBox.Multiline = true;
            this.fullStudentTextBox.Name = "fullStudentTextBox";
            this.fullStudentTextBox.Size = new System.Drawing.Size(718, 406);
            this.fullStudentTextBox.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(315, 378);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 25);
            this.label4.TabIndex = 17;
            this.label4.Text = "BasicStudent";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1185, 378);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 25);
            this.label5.TabIndex = 18;
            this.label5.Text = "FullStudent";
            // 
            // GetRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1666, 924);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.fullStudentTextBox);
            this.Controls.Add(this.basicStudentTextBox);
            this.Controls.Add(this.retrieveStudentButton);
            this.Controls.Add(this.lastName);
            this.Controls.Add(this.middleName);
            this.Controls.Add(this.firstName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "GetRecordForm";
            this.Text = "GetRecordForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox lastName;
        private System.Windows.Forms.TextBox middleName;
        private System.Windows.Forms.TextBox firstName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button retrieveStudentButton;
 
        private System.Windows.Forms.TextBox basicStudentTextBox;
        private System.Windows.Forms.TextBox fullStudentTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}