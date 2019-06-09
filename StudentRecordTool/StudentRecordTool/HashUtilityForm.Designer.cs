namespace StudentRecordTool
{
    partial class HashUtilityForm
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
            this.shaRadioButton = new System.Windows.Forms.RadioButton();
            this.bcryptRadioButton = new System.Windows.Forms.RadioButton();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.outputTextbox = new System.Windows.Forms.TextBox();
            this.generateButton = new System.Windows.Forms.Button();
            this.bcryptNumeric = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.bcryptNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // shaRadioButton
            // 
            this.shaRadioButton.AutoSize = true;
            this.shaRadioButton.Location = new System.Drawing.Point(217, 482);
            this.shaRadioButton.Name = "shaRadioButton";
            this.shaRadioButton.Size = new System.Drawing.Size(122, 29);
            this.shaRadioButton.TabIndex = 0;
            this.shaRadioButton.TabStop = true;
            this.shaRadioButton.Text = "SHA512";
            this.shaRadioButton.UseVisualStyleBackColor = true;
            // 
            // bcryptRadioButton
            // 
            this.bcryptRadioButton.AutoSize = true;
            this.bcryptRadioButton.Location = new System.Drawing.Point(365, 482);
            this.bcryptRadioButton.Name = "bcryptRadioButton";
            this.bcryptRadioButton.Size = new System.Drawing.Size(108, 29);
            this.bcryptRadioButton.TabIndex = 1;
            this.bcryptRadioButton.TabStop = true;
            this.bcryptRadioButton.Text = "BCrypt";
            this.bcryptRadioButton.UseVisualStyleBackColor = true;
            // 
            // inputTextBox
            // 
            this.inputTextBox.Location = new System.Drawing.Point(31, 30);
            this.inputTextBox.Multiline = true;
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(952, 411);
            this.inputTextBox.TabIndex = 2;
            // 
            // outputTextbox
            // 
            this.outputTextbox.Location = new System.Drawing.Point(31, 546);
            this.outputTextbox.Multiline = true;
            this.outputTextbox.Name = "outputTextbox";
            this.outputTextbox.Size = new System.Drawing.Size(952, 411);
            this.outputTextbox.TabIndex = 3;
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(681, 470);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(191, 53);
            this.generateButton.TabIndex = 4;
            this.generateButton.Text = "Generate";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // bcryptNumeric
            // 
            this.bcryptNumeric.Location = new System.Drawing.Point(480, 482);
            this.bcryptNumeric.Name = "bcryptNumeric";
            this.bcryptNumeric.Size = new System.Drawing.Size(120, 31);
            this.bcryptNumeric.TabIndex = 5;
            this.bcryptNumeric.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            // 
            // HashUtilityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 988);
            this.Controls.Add(this.bcryptNumeric);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.outputTextbox);
            this.Controls.Add(this.inputTextBox);
            this.Controls.Add(this.bcryptRadioButton);
            this.Controls.Add(this.shaRadioButton);
            this.Name = "HashUtilityForm";
            this.Text = "HashUtilityForm";
            ((System.ComponentModel.ISupportInitialize)(this.bcryptNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton shaRadioButton;
        private System.Windows.Forms.RadioButton bcryptRadioButton;
        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.TextBox outputTextbox;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.NumericUpDown bcryptNumeric;
    }
}