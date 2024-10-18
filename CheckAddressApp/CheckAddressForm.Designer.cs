namespace CheckAddressApp
{
    partial class CheckAddressForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            addressTextBox = new TextBox();
            label1 = new Label();
            checkButton = new Button();
            label2 = new Label();
            googleResponseOutputTextBox = new RichTextBox();
            regionCodeComboBox = new ComboBox();
            localityTextBox = new TextBox();
            label3 = new Label();
            googleResponseFormattedAddressTextBox = new TextBox();
            label4 = new Label();
            label6 = new Label();
            postalCodeTextBox = new TextBox();
            label7 = new Label();
            label8 = new Label();
            sublocalityTextBox = new TextBox();
            label10 = new Label();
            administrativeAreaTextBox = new TextBox();
            label5 = new Label();
            label12 = new Label();
            googleResponseAdministrativeAreaTextBox = new TextBox();
            label13 = new Label();
            googleResponseSortingCodeTextBox = new TextBox();
            label14 = new Label();
            label15 = new Label();
            googleResponsePostalCodeTextBox = new TextBox();
            label16 = new Label();
            googleResponseSublocalityTextBox = new TextBox();
            label17 = new Label();
            googleResponseLocalityTextBox = new TextBox();
            googleResponseRegionCodeTextBox = new TextBox();
            googleResponseLanguageCodeTextBox = new TextBox();
            button1 = new Button();
            button2 = new Button();
            apiTabControl = new TabControl();
            googleMapsTabPage = new TabPage();
            loqateTabPage = new TabPage();
            button3 = new Button();
            loqateResponseVerificationLavelTextBox = new TextBox();
            label19 = new Label();
            label18 = new Label();
            loqateResponseDataGridView = new DataGridView();
            fieldColumn = new DataGridViewTextBoxColumn();
            valueColumn = new DataGridViewTextBoxColumn();
            label43 = new Label();
            loqateResponseListBox = new ListBox();
            groupBox1 = new GroupBox();
            inputGroupBox = new GroupBox();
            label9 = new Label();
            requestAddressTextBox = new TextBox();
            apiGroupBox = new GroupBox();
            loqateCheckBox = new CheckBox();
            googleMapsCheckBox = new CheckBox();
            autocompleteButton = new Button();
            apiTabControl.SuspendLayout();
            googleMapsTabPage.SuspendLayout();
            loqateTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)loqateResponseDataGridView).BeginInit();
            groupBox1.SuspendLayout();
            inputGroupBox.SuspendLayout();
            apiGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // addressTextBox
            // 
            addressTextBox.Location = new Point(16, 50);
            addressTextBox.Name = "addressTextBox";
            addressTextBox.Size = new Size(344, 27);
            addressTextBox.TabIndex = 0;
            addressTextBox.TextChanged += addressTextBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 27);
            label1.Name = "label1";
            label1.Size = new Size(62, 20);
            label1.TabIndex = 1;
            label1.Text = "Address";
            // 
            // checkButton
            // 
            checkButton.Enabled = false;
            checkButton.Location = new Point(532, 160);
            checkButton.Name = "checkButton";
            checkButton.Size = new Size(95, 39);
            checkButton.TabIndex = 2;
            checkButton.Text = "Check";
            checkButton.UseVisualStyleBackColor = true;
            checkButton.Click += checkButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(561, 63);
            label2.Name = "label2";
            label2.Size = new Size(55, 20);
            label2.TabIndex = 4;
            label2.Text = "Output";
            // 
            // googleResponseOutputTextBox
            // 
            googleResponseOutputTextBox.Location = new Point(561, 86);
            googleResponseOutputTextBox.Name = "googleResponseOutputTextBox";
            googleResponseOutputTextBox.ReadOnly = true;
            googleResponseOutputTextBox.Size = new Size(501, 353);
            googleResponseOutputTextBox.TabIndex = 5;
            googleResponseOutputTextBox.Text = "";
            // 
            // regionCodeComboBox
            // 
            regionCodeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            regionCodeComboBox.FormattingEnabled = true;
            regionCodeComboBox.Items.AddRange(new object[] { "US", "DE" });
            regionCodeComboBox.Location = new Point(532, 49);
            regionCodeComboBox.Name = "regionCodeComboBox";
            regionCodeComboBox.Size = new Size(95, 28);
            regionCodeComboBox.TabIndex = 6;
            // 
            // localityTextBox
            // 
            localityTextBox.Location = new Point(16, 109);
            localityTextBox.Name = "localityTextBox";
            localityTextBox.Size = new Size(169, 27);
            localityTextBox.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 85);
            label3.Name = "label3";
            label3.Size = new Size(60, 20);
            label3.TabIndex = 8;
            label3.Text = "Locality";
            // 
            // googleResponseFormattedAddressTextBox
            // 
            googleResponseFormattedAddressTextBox.Location = new Point(6, 30);
            googleResponseFormattedAddressTextBox.Name = "googleResponseFormattedAddressTextBox";
            googleResponseFormattedAddressTextBox.ReadOnly = true;
            googleResponseFormattedAddressTextBox.Size = new Size(554, 27);
            googleResponseFormattedAddressTextBox.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 7);
            label4.Name = "label4";
            label4.Size = new Size(135, 20);
            label4.TabIndex = 10;
            label4.Text = "Formatted Address";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(413, 27);
            label6.Name = "label6";
            label6.Size = new Size(87, 20);
            label6.TabIndex = 14;
            label6.Text = "Postal Code";
            // 
            // postalCodeTextBox
            // 
            postalCodeTextBox.Location = new Point(413, 50);
            postalCodeTextBox.Name = "postalCodeTextBox";
            postalCodeTextBox.Size = new Size(113, 27);
            postalCodeTextBox.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(532, 26);
            label7.Name = "label7";
            label7.Size = new Size(95, 20);
            label7.TabIndex = 15;
            label7.Text = "Region Code";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(191, 85);
            label8.Name = "label8";
            label8.Size = new Size(82, 20);
            label8.TabIndex = 17;
            label8.Text = "Sublocality";
            // 
            // sublocalityTextBox
            // 
            sublocalityTextBox.Location = new Point(191, 109);
            sublocalityTextBox.Name = "sublocalityTextBox";
            sublocalityTextBox.Size = new Size(169, 27);
            sublocalityTextBox.TabIndex = 16;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(413, 85);
            label10.Name = "label10";
            label10.Size = new Size(140, 20);
            label10.TabIndex = 21;
            label10.Text = "Administrative Area";
            // 
            // administrativeAreaTextBox
            // 
            administrativeAreaTextBox.Location = new Point(413, 109);
            administrativeAreaTextBox.Name = "administrativeAreaTextBox";
            administrativeAreaTextBox.Size = new Size(214, 27);
            administrativeAreaTextBox.TabIndex = 20;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(7, 275);
            label5.Name = "label5";
            label5.Size = new Size(113, 20);
            label5.TabIndex = 33;
            label5.Text = "Language Code";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(7, 383);
            label12.Name = "label12";
            label12.Size = new Size(140, 20);
            label12.TabIndex = 31;
            label12.Text = "Administrative Area";
            // 
            // googleResponseAdministrativeAreaTextBox
            // 
            googleResponseAdministrativeAreaTextBox.Location = new Point(7, 407);
            googleResponseAdministrativeAreaTextBox.Name = "googleResponseAdministrativeAreaTextBox";
            googleResponseAdministrativeAreaTextBox.ReadOnly = true;
            googleResponseAdministrativeAreaTextBox.Size = new Size(168, 27);
            googleResponseAdministrativeAreaTextBox.TabIndex = 30;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(7, 329);
            label13.Name = "label13";
            label13.Size = new Size(96, 20);
            label13.TabIndex = 29;
            label13.Text = "Sorting Code";
            // 
            // googleResponseSortingCodeTextBox
            // 
            googleResponseSortingCodeTextBox.Location = new Point(7, 353);
            googleResponseSortingCodeTextBox.Name = "googleResponseSortingCodeTextBox";
            googleResponseSortingCodeTextBox.ReadOnly = true;
            googleResponseSortingCodeTextBox.Size = new Size(168, 27);
            googleResponseSortingCodeTextBox.TabIndex = 28;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(6, 222);
            label14.Name = "label14";
            label14.Size = new Size(95, 20);
            label14.TabIndex = 27;
            label14.Text = "Region Code";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(6, 169);
            label15.Name = "label15";
            label15.Size = new Size(87, 20);
            label15.TabIndex = 26;
            label15.Text = "Postal Code";
            // 
            // googleResponsePostalCodeTextBox
            // 
            googleResponsePostalCodeTextBox.Location = new Point(6, 192);
            googleResponsePostalCodeTextBox.Name = "googleResponsePostalCodeTextBox";
            googleResponsePostalCodeTextBox.ReadOnly = true;
            googleResponsePostalCodeTextBox.Size = new Size(169, 27);
            googleResponsePostalCodeTextBox.TabIndex = 25;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(6, 116);
            label16.Name = "label16";
            label16.Size = new Size(82, 20);
            label16.TabIndex = 37;
            label16.Text = "Sublocality";
            // 
            // googleResponseSublocalityTextBox
            // 
            googleResponseSublocalityTextBox.Location = new Point(6, 139);
            googleResponseSublocalityTextBox.Name = "googleResponseSublocalityTextBox";
            googleResponseSublocalityTextBox.ReadOnly = true;
            googleResponseSublocalityTextBox.Size = new Size(169, 27);
            googleResponseSublocalityTextBox.TabIndex = 36;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(6, 63);
            label17.Name = "label17";
            label17.Size = new Size(60, 20);
            label17.TabIndex = 35;
            label17.Text = "Locality";
            // 
            // googleResponseLocalityTextBox
            // 
            googleResponseLocalityTextBox.Location = new Point(6, 86);
            googleResponseLocalityTextBox.Name = "googleResponseLocalityTextBox";
            googleResponseLocalityTextBox.ReadOnly = true;
            googleResponseLocalityTextBox.Size = new Size(169, 27);
            googleResponseLocalityTextBox.TabIndex = 34;
            // 
            // googleResponseRegionCodeTextBox
            // 
            googleResponseRegionCodeTextBox.Location = new Point(6, 245);
            googleResponseRegionCodeTextBox.Name = "googleResponseRegionCodeTextBox";
            googleResponseRegionCodeTextBox.ReadOnly = true;
            googleResponseRegionCodeTextBox.Size = new Size(169, 27);
            googleResponseRegionCodeTextBox.TabIndex = 38;
            // 
            // googleResponseLanguageCodeTextBox
            // 
            googleResponseLanguageCodeTextBox.Location = new Point(7, 299);
            googleResponseLanguageCodeTextBox.Name = "googleResponseLanguageCodeTextBox";
            googleResponseLanguageCodeTextBox.ReadOnly = true;
            googleResponseLanguageCodeTextBox.Size = new Size(168, 27);
            googleResponseLanguageCodeTextBox.TabIndex = 39;
            // 
            // button1
            // 
            button1.Location = new Point(968, 24);
            button1.Name = "button1";
            button1.Size = new Size(94, 39);
            button1.TabIndex = 41;
            button1.Text = "Clear";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(633, 160);
            button2.Name = "button2";
            button2.Size = new Size(95, 39);
            button2.TabIndex = 42;
            button2.Text = "Clear";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // apiTabControl
            // 
            apiTabControl.Controls.Add(googleMapsTabPage);
            apiTabControl.Controls.Add(loqateTabPage);
            apiTabControl.Location = new Point(16, 26);
            apiTabControl.Name = "apiTabControl";
            apiTabControl.SelectedIndex = 0;
            apiTabControl.Size = new Size(1076, 478);
            apiTabControl.TabIndex = 43;
            // 
            // googleMapsTabPage
            // 
            googleMapsTabPage.Controls.Add(googleResponseOutputTextBox);
            googleMapsTabPage.Controls.Add(label2);
            googleMapsTabPage.Controls.Add(button1);
            googleMapsTabPage.Controls.Add(googleResponseFormattedAddressTextBox);
            googleMapsTabPage.Controls.Add(label4);
            googleMapsTabPage.Controls.Add(googleResponseLanguageCodeTextBox);
            googleMapsTabPage.Controls.Add(googleResponsePostalCodeTextBox);
            googleMapsTabPage.Controls.Add(googleResponseRegionCodeTextBox);
            googleMapsTabPage.Controls.Add(label15);
            googleMapsTabPage.Controls.Add(label16);
            googleMapsTabPage.Controls.Add(label14);
            googleMapsTabPage.Controls.Add(googleResponseSublocalityTextBox);
            googleMapsTabPage.Controls.Add(googleResponseSortingCodeTextBox);
            googleMapsTabPage.Controls.Add(label17);
            googleMapsTabPage.Controls.Add(label13);
            googleMapsTabPage.Controls.Add(googleResponseLocalityTextBox);
            googleMapsTabPage.Controls.Add(googleResponseAdministrativeAreaTextBox);
            googleMapsTabPage.Controls.Add(label5);
            googleMapsTabPage.Controls.Add(label12);
            googleMapsTabPage.Location = new Point(4, 29);
            googleMapsTabPage.Name = "googleMapsTabPage";
            googleMapsTabPage.Padding = new Padding(3);
            googleMapsTabPage.Size = new Size(1068, 445);
            googleMapsTabPage.TabIndex = 0;
            googleMapsTabPage.Text = "Google Maps";
            googleMapsTabPage.UseVisualStyleBackColor = true;
            // 
            // loqateTabPage
            // 
            loqateTabPage.Controls.Add(button3);
            loqateTabPage.Controls.Add(loqateResponseVerificationLavelTextBox);
            loqateTabPage.Controls.Add(label19);
            loqateTabPage.Controls.Add(label18);
            loqateTabPage.Controls.Add(loqateResponseDataGridView);
            loqateTabPage.Controls.Add(label43);
            loqateTabPage.Controls.Add(loqateResponseListBox);
            loqateTabPage.Location = new Point(4, 29);
            loqateTabPage.Name = "loqateTabPage";
            loqateTabPage.Padding = new Padding(3);
            loqateTabPage.Size = new Size(1068, 445);
            loqateTabPage.TabIndex = 1;
            loqateTabPage.Text = "Loqate";
            loqateTabPage.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(968, 215);
            button3.Name = "button3";
            button3.Size = new Size(94, 39);
            button3.TabIndex = 82;
            button3.Text = "Clear";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // loqateResponseVerificationLavelTextBox
            // 
            loqateResponseVerificationLavelTextBox.Location = new Point(592, 227);
            loqateResponseVerificationLavelTextBox.Name = "loqateResponseVerificationLavelTextBox";
            loqateResponseVerificationLavelTextBox.ReadOnly = true;
            loqateResponseVerificationLavelTextBox.Size = new Size(125, 27);
            loqateResponseVerificationLavelTextBox.TabIndex = 81;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(591, 204);
            label19.Name = "label19";
            label19.Size = new Size(119, 20);
            label19.TabIndex = 80;
            label19.Text = "Verification lavel";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(7, 13);
            label18.Name = "label18";
            label18.Size = new Size(50, 20);
            label18.TabIndex = 79;
            label18.Text = "Match";
            // 
            // loqateResponseDataGridView
            // 
            loqateResponseDataGridView.AllowUserToAddRows = false;
            loqateResponseDataGridView.AllowUserToDeleteRows = false;
            loqateResponseDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            loqateResponseDataGridView.Columns.AddRange(new DataGridViewColumn[] { fieldColumn, valueColumn });
            loqateResponseDataGridView.Location = new Point(7, 36);
            loqateResponseDataGridView.Name = "loqateResponseDataGridView";
            loqateResponseDataGridView.ReadOnly = true;
            loqateResponseDataGridView.RowHeadersWidth = 51;
            loqateResponseDataGridView.Size = new Size(555, 403);
            loqateResponseDataGridView.TabIndex = 78;
            // 
            // fieldColumn
            // 
            fieldColumn.HeaderText = "Field";
            fieldColumn.MinimumWidth = 6;
            fieldColumn.Name = "fieldColumn";
            fieldColumn.ReadOnly = true;
            fieldColumn.Width = 200;
            // 
            // valueColumn
            // 
            valueColumn.HeaderText = "Value";
            valueColumn.MinimumWidth = 6;
            valueColumn.Name = "valueColumn";
            valueColumn.ReadOnly = true;
            valueColumn.Width = 300;
            // 
            // label43
            // 
            label43.AutoSize = true;
            label43.Location = new Point(592, 13);
            label43.Name = "label43";
            label43.Size = new Size(64, 20);
            label43.TabIndex = 75;
            label43.Text = "Matches";
            // 
            // loqateResponseListBox
            // 
            loqateResponseListBox.FormattingEnabled = true;
            loqateResponseListBox.HorizontalScrollbar = true;
            loqateResponseListBox.Location = new Point(592, 36);
            loqateResponseListBox.Name = "loqateResponseListBox";
            loqateResponseListBox.Size = new Size(470, 144);
            loqateResponseListBox.TabIndex = 25;
            loqateResponseListBox.SelectedIndexChanged += loqateResponseListBox_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(apiTabControl);
            groupBox1.Location = new Point(12, 312);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1105, 518);
            groupBox1.TabIndex = 44;
            groupBox1.TabStop = false;
            groupBox1.Text = "Response";
            // 
            // inputGroupBox
            // 
            inputGroupBox.Controls.Add(label9);
            inputGroupBox.Controls.Add(requestAddressTextBox);
            inputGroupBox.Controls.Add(apiGroupBox);
            inputGroupBox.Controls.Add(autocompleteButton);
            inputGroupBox.Controls.Add(addressTextBox);
            inputGroupBox.Controls.Add(label1);
            inputGroupBox.Controls.Add(button2);
            inputGroupBox.Controls.Add(checkButton);
            inputGroupBox.Controls.Add(regionCodeComboBox);
            inputGroupBox.Controls.Add(localityTextBox);
            inputGroupBox.Controls.Add(label10);
            inputGroupBox.Controls.Add(label3);
            inputGroupBox.Controls.Add(administrativeAreaTextBox);
            inputGroupBox.Controls.Add(postalCodeTextBox);
            inputGroupBox.Controls.Add(label6);
            inputGroupBox.Controls.Add(label7);
            inputGroupBox.Controls.Add(label8);
            inputGroupBox.Controls.Add(sublocalityTextBox);
            inputGroupBox.Location = new Point(12, 12);
            inputGroupBox.Name = "inputGroupBox";
            inputGroupBox.Size = new Size(1105, 294);
            inputGroupBox.TabIndex = 45;
            inputGroupBox.TabStop = false;
            inputGroupBox.Text = "Input";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(16, 229);
            label9.Name = "label9";
            label9.Size = new Size(117, 20);
            label9.TabIndex = 46;
            label9.Text = "Request address";
            // 
            // requestAddressTextBox
            // 
            requestAddressTextBox.Location = new Point(16, 252);
            requestAddressTextBox.Name = "requestAddressTextBox";
            requestAddressTextBox.ReadOnly = true;
            requestAddressTextBox.Size = new Size(1076, 27);
            requestAddressTextBox.TabIndex = 45;
            // 
            // apiGroupBox
            // 
            apiGroupBox.Controls.Add(loqateCheckBox);
            apiGroupBox.Controls.Add(googleMapsCheckBox);
            apiGroupBox.Location = new Point(16, 142);
            apiGroupBox.Name = "apiGroupBox";
            apiGroupBox.Size = new Size(213, 68);
            apiGroupBox.TabIndex = 44;
            apiGroupBox.TabStop = false;
            apiGroupBox.Text = "Api";
            // 
            // loqateCheckBox
            // 
            loqateCheckBox.AutoSize = true;
            loqateCheckBox.Location = new Point(132, 26);
            loqateCheckBox.Name = "loqateCheckBox";
            loqateCheckBox.Size = new Size(77, 24);
            loqateCheckBox.TabIndex = 1;
            loqateCheckBox.Text = "Loqate";
            loqateCheckBox.UseVisualStyleBackColor = true;
            // 
            // googleMapsCheckBox
            // 
            googleMapsCheckBox.AutoSize = true;
            googleMapsCheckBox.Location = new Point(6, 26);
            googleMapsCheckBox.Name = "googleMapsCheckBox";
            googleMapsCheckBox.Size = new Size(120, 24);
            googleMapsCheckBox.TabIndex = 0;
            googleMapsCheckBox.Text = "Google Maps";
            googleMapsCheckBox.UseVisualStyleBackColor = true;
            // 
            // autocompleteButton
            // 
            autocompleteButton.Enabled = false;
            autocompleteButton.Location = new Point(413, 160);
            autocompleteButton.Name = "autocompleteButton";
            autocompleteButton.Size = new Size(113, 39);
            autocompleteButton.TabIndex = 43;
            autocompleteButton.Text = "Autocomplete";
            autocompleteButton.UseVisualStyleBackColor = true;
            autocompleteButton.Click += autocompleteButton_Click;
            // 
            // CheckAddressForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1132, 842);
            Controls.Add(inputGroupBox);
            Controls.Add(groupBox1);
            Name = "CheckAddressForm";
            Text = "CheckAddressForm";
            apiTabControl.ResumeLayout(false);
            googleMapsTabPage.ResumeLayout(false);
            googleMapsTabPage.PerformLayout();
            loqateTabPage.ResumeLayout(false);
            loqateTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)loqateResponseDataGridView).EndInit();
            groupBox1.ResumeLayout(false);
            inputGroupBox.ResumeLayout(false);
            inputGroupBox.PerformLayout();
            apiGroupBox.ResumeLayout(false);
            apiGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox addressTextBox;
        private Label label1;
        private Button checkButton;
        private Label label2;
        private RichTextBox googleResponseOutputTextBox;
        private ComboBox regionCodeComboBox;
        private TextBox localityTextBox;
        private Label label3;
        private TextBox googleResponseFormattedAddressTextBox;
        private Label label4;
        private Label label6;
        private TextBox postalCodeTextBox;
        private Label label7;
        private Label label8;
        private TextBox sublocalityTextBox;
        private Label label10;
        private TextBox administrativeAreaTextBox;
        private Label label5;
        private Label label12;
        private TextBox googleResponseAdministrativeAreaTextBox;
        private Label label13;
        private TextBox googleResponseSortingCodeTextBox;
        private Label label14;
        private Label label15;
        private TextBox googleResponsePostalCodeTextBox;
        private Label label16;
        private TextBox googleResponseSublocalityTextBox;
        private Label label17;
        private TextBox googleResponseLocalityTextBox;
        private TextBox googleResponseRegionCodeTextBox;
        private TextBox googleResponseLanguageCodeTextBox;
        private Button button1;
        private Button button2;
        private TabControl apiTabControl;
        private TabPage googleMapsTabPage;
        private TabPage loqateTabPage;
        private GroupBox groupBox1;
        private GroupBox inputGroupBox;
        private Button autocompleteButton;
        private GroupBox apiGroupBox;
        private CheckBox loqateCheckBox;
        private CheckBox googleMapsCheckBox;
        private ListBox loqateResponseListBox;
        private Label label43;
        private Label label38;
        private TextBox textBox21;
        private Label label39;
        private TextBox textBox22;
        private Label label40;
        private TextBox textBox23;
        private Label label41;
        private TextBox textBox24;
        private Label label42;
        private TextBox textBox25;
        private Label label33;
        private TextBox textBox16;
        private Label label34;
        private TextBox textBox17;
        private Label label35;
        private TextBox textBox18;
        private Label label36;
        private TextBox textBox19;
        private Label label37;
        private TextBox textBox20;
        private Label label28;
        private TextBox textBox11;
        private Label label29;
        private TextBox textBox12;
        private Label label30;
        private TextBox textBox13;
        private Label label31;
        private TextBox textBox14;
        private Label label32;
        private TextBox textBox15;
        private Label label23;
        private TextBox textBox6;
        private Label label24;
        private TextBox textBox7;
        private Label label25;
        private TextBox textBox8;
        private Label label26;
        private TextBox textBox9;
        private Label label27;
        private TextBox textBox10;
        private Label label22;
        private TextBox textBox5;
        private Label label21;
        private TextBox textBox4;
        private Label label20;
        private TextBox textBox3;
        private Label label44;
        private TextBox textBox26;
        private DataGridView loqateResponseDataGridView;
        private Label label18;
        private DataGridViewTextBoxColumn fieldColumn;
        private DataGridViewTextBoxColumn valueColumn;
        private TextBox loqateResponseVerificationLavelTextBox;
        private Label label19;
        private Button button3;
        private TextBox requestAddressTextBox;
        private Label label9;
    }
}
