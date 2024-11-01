namespace CheckAddressApp
{
    partial class ErrorForm
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
            ErrorRichTextBox = new RichTextBox();
            errorGroupBox = new GroupBox();
            errorGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // ErrorRichTextBox
            // 
            ErrorRichTextBox.BorderStyle = BorderStyle.None;
            ErrorRichTextBox.Font = new Font("Segoe UI", 9F);
            ErrorRichTextBox.ForeColor = Color.FromArgb(192, 0, 0);
            ErrorRichTextBox.Location = new Point(22, 35);
            ErrorRichTextBox.Name = "ErrorRichTextBox";
            ErrorRichTextBox.ReadOnly = true;
            ErrorRichTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            ErrorRichTextBox.Size = new Size(582, 373);
            ErrorRichTextBox.TabIndex = 0;
            ErrorRichTextBox.Text = "";
            // 
            // errorGroupBox
            // 
            errorGroupBox.Controls.Add(ErrorRichTextBox);
            errorGroupBox.Font = new Font("Segoe UI", 13F);
            errorGroupBox.ForeColor = Color.FromArgb(192, 0, 0);
            errorGroupBox.Location = new Point(12, 12);
            errorGroupBox.Name = "errorGroupBox";
            errorGroupBox.Size = new Size(626, 426);
            errorGroupBox.TabIndex = 1;
            errorGroupBox.TabStop = false;
            errorGroupBox.Text = "Error text";
            // 
            // ErrorForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(647, 450);
            Controls.Add(errorGroupBox);
            Name = "ErrorForm";
            Text = "Error";
            errorGroupBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox ErrorRichTextBox;
        private GroupBox errorGroupBox;
    }
}