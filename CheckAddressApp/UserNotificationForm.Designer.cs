namespace qAcProviderTest
{
    partial class UserNotificationForm
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
            userNotificationRichTextBox = new RichTextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // userNotificationRichTextBox
            // 
            userNotificationRichTextBox.BorderStyle = BorderStyle.None;
            userNotificationRichTextBox.Font = new Font("Segoe UI", 15F);
            userNotificationRichTextBox.ForeColor = Color.FromArgb(192, 0, 0);
            userNotificationRichTextBox.Location = new Point(12, 12);
            userNotificationRichTextBox.Name = "userNotificationRichTextBox";
            userNotificationRichTextBox.ReadOnly = true;
            userNotificationRichTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            userNotificationRichTextBox.Size = new Size(620, 226);
            userNotificationRichTextBox.TabIndex = 1;
            userNotificationRichTextBox.Text = "";
            // 
            // button1
            // 
            button1.Location = new Point(534, 244);
            button1.Name = "button1";
            button1.Size = new Size(97, 41);
            button1.TabIndex = 2;
            button1.Text = "Ok";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // UserNotificationForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(643, 297);
            ControlBox = false;
            Controls.Add(button1);
            Controls.Add(userNotificationRichTextBox);
            FormBorderStyle = FormBorderStyle.None;
            Name = "UserNotificationForm";
            ShowIcon = false;
            Text = "User Notification";
            VisibleChanged += UserNotificationForm_VisibleChanged;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox userNotificationRichTextBox;
        private Button button1;
    }
}