﻿namespace CheckAddressApp
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            streetAndHouseNumberTextBox = new TextBox();
            label1 = new Label();
            checkButton = new Button();
            label2 = new Label();
            googleResponseOutputTextBox = new RichTextBox();
            countryCodeComboBox = new ComboBox();
            cityTextBox = new TextBox();
            label3 = new Label();
            googleResponseFormattedAddressTextBox = new TextBox();
            label4 = new Label();
            label6 = new Label();
            postalCodeTextBox = new TextBox();
            label7 = new Label();
            label8 = new Label();
            districtTextBox = new TextBox();
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
            googleResponseStreetTextBox = new TextBox();
            label45 = new Label();
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
            tabPage1 = new TabPage();
            button4 = new Button();
            label47 = new Label();
            label46 = new Label();
            label11 = new Label();
            smartyResponseComponetsDataGridView = new DataGridView();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            smartyResponseAnalisisDataGridView = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            smartyResponseListBox = new ListBox();
            groupBox1 = new GroupBox();
            inputGroupBox = new GroupBox();
            inputsChoiceTabControl = new TabControl();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            freeInputTextBox = new TextBox();
            label10 = new Label();
            autocompleteAutosuggestSplitButton = new Controls.SplitButton();
            contextMenuStrip1 = new ContextMenuStrip(components);
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            label9 = new Label();
            requestAddressTextBox = new TextBox();
            apiGroupBox = new GroupBox();
            smartyCheckBox = new CheckBox();
            loqateCheckBox = new CheckBox();
            googleMapsCheckBox = new CheckBox();
            inputErrorProvider = new ErrorProvider(components);
            apiChoiceErrorProvider = new ErrorProvider(components);
            smartyCountryCodeErrorProvider = new ErrorProvider(components);
            apiTabControl.SuspendLayout();
            googleMapsTabPage.SuspendLayout();
            loqateTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)loqateResponseDataGridView).BeginInit();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)smartyResponseComponetsDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)smartyResponseAnalisisDataGridView).BeginInit();
            groupBox1.SuspendLayout();
            inputGroupBox.SuspendLayout();
            inputsChoiceTabControl.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            apiGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)inputErrorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)apiChoiceErrorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)smartyCountryCodeErrorProvider).BeginInit();
            SuspendLayout();
            // 
            // streetAndHouseNumberTextBox
            // 
            streetAndHouseNumberTextBox.Location = new Point(6, 40);
            streetAndHouseNumberTextBox.Name = "streetAndHouseNumberTextBox";
            streetAndHouseNumberTextBox.Size = new Size(238, 27);
            streetAndHouseNumberTextBox.TabIndex = 0;
            streetAndHouseNumberTextBox.TextChanged += addressTextBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 17);
            label1.Name = "label1";
            label1.Size = new Size(178, 20);
            label1.TabIndex = 1;
            label1.Text = "Street and House number";
            // 
            // checkButton
            // 
            checkButton.Location = new Point(896, 156);
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
            // countryCodeComboBox
            // 
            countryCodeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            countryCodeComboBox.FormattingEnabled = true;
            countryCodeComboBox.Items.AddRange(new object[] { "US", "DE" });
            countryCodeComboBox.Location = new Point(399, 95);
            countryCodeComboBox.Name = "countryCodeComboBox";
            countryCodeComboBox.Size = new Size(95, 28);
            countryCodeComboBox.TabIndex = 6;
            countryCodeComboBox.SelectedIndexChanged += regionCodeComboBox_SelectedIndexChanged;
            // 
            // cityTextBox
            // 
            cityTextBox.Location = new Point(6, 99);
            cityTextBox.Name = "cityTextBox";
            cityTextBox.Size = new Size(169, 27);
            cityTextBox.TabIndex = 7;
            cityTextBox.TextChanged += cityTextBox_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 75);
            label3.Name = "label3";
            label3.Size = new Size(34, 20);
            label3.TabIndex = 8;
            label3.Text = "City";
            // 
            // googleResponseFormattedAddressTextBox
            // 
            googleResponseFormattedAddressTextBox.Location = new Point(6, 36);
            googleResponseFormattedAddressTextBox.Name = "googleResponseFormattedAddressTextBox";
            googleResponseFormattedAddressTextBox.ReadOnly = true;
            googleResponseFormattedAddressTextBox.Size = new Size(527, 27);
            googleResponseFormattedAddressTextBox.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 13);
            label4.Name = "label4";
            label4.Size = new Size(135, 20);
            label4.TabIndex = 10;
            label4.Text = "Formatted Address";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(250, 17);
            label6.Name = "label6";
            label6.Size = new Size(87, 20);
            label6.TabIndex = 14;
            label6.Text = "Postal Code";
            // 
            // postalCodeTextBox
            // 
            postalCodeTextBox.Location = new Point(250, 40);
            postalCodeTextBox.Name = "postalCodeTextBox";
            postalCodeTextBox.Size = new Size(113, 27);
            postalCodeTextBox.TabIndex = 13;
            postalCodeTextBox.TextChanged += postalCodeTextBox_TextChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(399, 72);
            label7.Name = "label7";
            label7.Size = new Size(99, 20);
            label7.TabIndex = 15;
            label7.Text = "Country Code";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(181, 75);
            label8.Name = "label8";
            label8.Size = new Size(56, 20);
            label8.TabIndex = 17;
            label8.Text = "District";
            // 
            // districtTextBox
            // 
            districtTextBox.Location = new Point(181, 99);
            districtTextBox.Name = "districtTextBox";
            districtTextBox.Size = new Size(182, 27);
            districtTextBox.TabIndex = 16;
            districtTextBox.TextChanged += districtTextBox_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(365, 142);
            label5.Name = "label5";
            label5.Size = new Size(113, 20);
            label5.TabIndex = 33;
            label5.Text = "Language Code";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(365, 79);
            label12.Name = "label12";
            label12.Size = new Size(140, 20);
            label12.TabIndex = 31;
            label12.Text = "Administrative Area";
            // 
            // googleResponseAdministrativeAreaTextBox
            // 
            googleResponseAdministrativeAreaTextBox.Location = new Point(365, 103);
            googleResponseAdministrativeAreaTextBox.Name = "googleResponseAdministrativeAreaTextBox";
            googleResponseAdministrativeAreaTextBox.ReadOnly = true;
            googleResponseAdministrativeAreaTextBox.Size = new Size(168, 27);
            googleResponseAdministrativeAreaTextBox.TabIndex = 30;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(365, 206);
            label13.Name = "label13";
            label13.Size = new Size(96, 20);
            label13.TabIndex = 29;
            label13.Text = "Sorting Code";
            // 
            // googleResponseSortingCodeTextBox
            // 
            googleResponseSortingCodeTextBox.Location = new Point(365, 230);
            googleResponseSortingCodeTextBox.Name = "googleResponseSortingCodeTextBox";
            googleResponseSortingCodeTextBox.ReadOnly = true;
            googleResponseSortingCodeTextBox.Size = new Size(168, 27);
            googleResponseSortingCodeTextBox.TabIndex = 28;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(6, 333);
            label14.Name = "label14";
            label14.Size = new Size(95, 20);
            label14.TabIndex = 27;
            label14.Text = "Region Code";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(6, 270);
            label15.Name = "label15";
            label15.Size = new Size(87, 20);
            label15.TabIndex = 26;
            label15.Text = "Postal Code";
            // 
            // googleResponsePostalCodeTextBox
            // 
            googleResponsePostalCodeTextBox.Location = new Point(6, 293);
            googleResponsePostalCodeTextBox.Name = "googleResponsePostalCodeTextBox";
            googleResponsePostalCodeTextBox.ReadOnly = true;
            googleResponsePostalCodeTextBox.Size = new Size(169, 27);
            googleResponsePostalCodeTextBox.TabIndex = 25;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(6, 206);
            label16.Name = "label16";
            label16.Size = new Size(82, 20);
            label16.TabIndex = 37;
            label16.Text = "Sublocality";
            // 
            // googleResponseSublocalityTextBox
            // 
            googleResponseSublocalityTextBox.Location = new Point(6, 229);
            googleResponseSublocalityTextBox.Name = "googleResponseSublocalityTextBox";
            googleResponseSublocalityTextBox.ReadOnly = true;
            googleResponseSublocalityTextBox.Size = new Size(169, 27);
            googleResponseSublocalityTextBox.TabIndex = 36;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(6, 137);
            label17.Name = "label17";
            label17.Size = new Size(60, 20);
            label17.TabIndex = 35;
            label17.Text = "Locality";
            // 
            // googleResponseLocalityTextBox
            // 
            googleResponseLocalityTextBox.Location = new Point(6, 166);
            googleResponseLocalityTextBox.Name = "googleResponseLocalityTextBox";
            googleResponseLocalityTextBox.ReadOnly = true;
            googleResponseLocalityTextBox.Size = new Size(169, 27);
            googleResponseLocalityTextBox.TabIndex = 34;
            // 
            // googleResponseRegionCodeTextBox
            // 
            googleResponseRegionCodeTextBox.Location = new Point(6, 356);
            googleResponseRegionCodeTextBox.Name = "googleResponseRegionCodeTextBox";
            googleResponseRegionCodeTextBox.ReadOnly = true;
            googleResponseRegionCodeTextBox.Size = new Size(169, 27);
            googleResponseRegionCodeTextBox.TabIndex = 38;
            // 
            // googleResponseLanguageCodeTextBox
            // 
            googleResponseLanguageCodeTextBox.Location = new Point(365, 166);
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
            button2.Location = new Point(997, 156);
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
            apiTabControl.Controls.Add(tabPage1);
            apiTabControl.Location = new Point(16, 26);
            apiTabControl.Name = "apiTabControl";
            apiTabControl.SelectedIndex = 0;
            apiTabControl.Size = new Size(1076, 478);
            apiTabControl.TabIndex = 43;
            // 
            // googleMapsTabPage
            // 
            googleMapsTabPage.Controls.Add(googleResponseStreetTextBox);
            googleMapsTabPage.Controls.Add(label45);
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
            // googleResponseStreetTextBox
            // 
            googleResponseStreetTextBox.Location = new Point(6, 103);
            googleResponseStreetTextBox.Name = "googleResponseStreetTextBox";
            googleResponseStreetTextBox.ReadOnly = true;
            googleResponseStreetTextBox.Size = new Size(169, 27);
            googleResponseStreetTextBox.TabIndex = 45;
            // 
            // label45
            // 
            label45.AutoSize = true;
            label45.Location = new Point(6, 79);
            label45.Name = "label45";
            label45.Size = new Size(48, 20);
            label45.TabIndex = 44;
            label45.Text = "Street";
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
            loqateResponseDataGridView.RowHeadersVisible = false;
            loqateResponseDataGridView.RowHeadersWidth = 51;
            loqateResponseDataGridView.RowTemplate.Height = 25;
            loqateResponseDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            loqateResponseDataGridView.Size = new Size(499, 403);
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
            // tabPage1
            // 
            tabPage1.Controls.Add(button4);
            tabPage1.Controls.Add(label47);
            tabPage1.Controls.Add(label46);
            tabPage1.Controls.Add(label11);
            tabPage1.Controls.Add(smartyResponseComponetsDataGridView);
            tabPage1.Controls.Add(smartyResponseAnalisisDataGridView);
            tabPage1.Controls.Add(smartyResponseListBox);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1068, 445);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "Smarty";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(968, 207);
            button4.Name = "button4";
            button4.Size = new Size(94, 39);
            button4.TabIndex = 84;
            button4.Text = "Clear";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label47
            // 
            label47.AutoSize = true;
            label47.Location = new Point(18, 14);
            label47.Name = "label47";
            label47.Size = new Size(93, 20);
            label47.TabIndex = 83;
            label47.Text = "Components";
            // 
            // label46
            // 
            label46.AutoSize = true;
            label46.Location = new Point(374, 14);
            label46.Name = "label46";
            label46.Size = new Size(62, 20);
            label46.TabIndex = 82;
            label46.Text = "Analysis";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(746, 14);
            label11.Name = "label11";
            label11.Size = new Size(64, 20);
            label11.TabIndex = 81;
            label11.Text = "Matches";
            // 
            // smartyResponseComponetsDataGridView
            // 
            smartyResponseComponetsDataGridView.AllowUserToAddRows = false;
            smartyResponseComponetsDataGridView.AllowUserToDeleteRows = false;
            smartyResponseComponetsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            smartyResponseComponetsDataGridView.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4 });
            smartyResponseComponetsDataGridView.Location = new Point(18, 37);
            smartyResponseComponetsDataGridView.Name = "smartyResponseComponetsDataGridView";
            smartyResponseComponetsDataGridView.ReadOnly = true;
            smartyResponseComponetsDataGridView.RowHeadersVisible = false;
            smartyResponseComponetsDataGridView.RowHeadersWidth = 51;
            smartyResponseComponetsDataGridView.RowTemplate.Height = 25;
            smartyResponseComponetsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            smartyResponseComponetsDataGridView.Size = new Size(350, 301);
            smartyResponseComponetsDataGridView.TabIndex = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewTextBoxColumn3.HeaderText = "Field";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Value";
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            dataGridViewTextBoxColumn4.Width = 300;
            // 
            // smartyResponseAnalisisDataGridView
            // 
            smartyResponseAnalisisDataGridView.AllowUserToAddRows = false;
            smartyResponseAnalisisDataGridView.AllowUserToDeleteRows = false;
            smartyResponseAnalisisDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            smartyResponseAnalisisDataGridView.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2 });
            smartyResponseAnalisisDataGridView.Location = new Point(374, 37);
            smartyResponseAnalisisDataGridView.Name = "smartyResponseAnalisisDataGridView";
            smartyResponseAnalisisDataGridView.ReadOnly = true;
            smartyResponseAnalisisDataGridView.RowHeadersVisible = false;
            smartyResponseAnalisisDataGridView.RowHeadersWidth = 51;
            smartyResponseAnalisisDataGridView.RowTemplate.Height = 25;
            smartyResponseAnalisisDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            smartyResponseAnalisisDataGridView.Size = new Size(350, 301);
            smartyResponseAnalisisDataGridView.TabIndex = 79;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Field";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Value";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            dataGridViewTextBoxColumn2.Width = 300;
            // 
            // smartyResponseListBox
            // 
            smartyResponseListBox.FormattingEnabled = true;
            smartyResponseListBox.HorizontalScrollbar = true;
            smartyResponseListBox.Location = new Point(746, 37);
            smartyResponseListBox.Name = "smartyResponseListBox";
            smartyResponseListBox.Size = new Size(316, 144);
            smartyResponseListBox.TabIndex = 26;
            smartyResponseListBox.SelectedIndexChanged += smartyResponseListBox_SelectedIndexChanged;
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
            inputGroupBox.Controls.Add(inputsChoiceTabControl);
            inputGroupBox.Controls.Add(autocompleteAutosuggestSplitButton);
            inputGroupBox.Controls.Add(label9);
            inputGroupBox.Controls.Add(requestAddressTextBox);
            inputGroupBox.Controls.Add(apiGroupBox);
            inputGroupBox.Controls.Add(button2);
            inputGroupBox.Controls.Add(checkButton);
            inputGroupBox.Controls.Add(countryCodeComboBox);
            inputGroupBox.Controls.Add(label7);
            inputGroupBox.Location = new Point(12, 12);
            inputGroupBox.Name = "inputGroupBox";
            inputGroupBox.Size = new Size(1105, 294);
            inputGroupBox.TabIndex = 45;
            inputGroupBox.TabStop = false;
            inputGroupBox.Text = "Input";
            // 
            // inputsChoiceTabControl
            // 
            inputsChoiceTabControl.Controls.Add(tabPage2);
            inputsChoiceTabControl.Controls.Add(tabPage3);
            inputsChoiceTabControl.Location = new Point(16, 26);
            inputsChoiceTabControl.Name = "inputsChoiceTabControl";
            inputsChoiceTabControl.SelectedIndex = 0;
            inputsChoiceTabControl.Size = new Size(377, 169);
            inputsChoiceTabControl.TabIndex = 48;
            inputsChoiceTabControl.SelectedIndexChanged += inputsChoice_SelectedIndexChanged;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.WhiteSmoke;
            tabPage2.Controls.Add(streetAndHouseNumberTextBox);
            tabPage2.Controls.Add(districtTextBox);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(postalCodeTextBox);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(cityTextBox);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(369, 136);
            tabPage2.TabIndex = 0;
            tabPage2.Text = "Structured Input";
            // 
            // tabPage3
            // 
            tabPage3.BackColor = Color.WhiteSmoke;
            tabPage3.Controls.Add(freeInputTextBox);
            tabPage3.Controls.Add(label10);
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(369, 136);
            tabPage3.TabIndex = 1;
            tabPage3.Text = "Free Input";
            // 
            // freeInputTextBox
            // 
            freeInputTextBox.Location = new Point(6, 40);
            freeInputTextBox.Name = "freeInputTextBox";
            freeInputTextBox.Size = new Size(357, 27);
            freeInputTextBox.TabIndex = 2;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 17);
            label10.Name = "label10";
            label10.Size = new Size(62, 20);
            label10.TabIndex = 3;
            label10.Text = "Address";
            // 
            // autocompleteAutosuggestSplitButton
            // 
            autocompleteAutosuggestSplitButton.ContextMenuStrip = contextMenuStrip1;
            autocompleteAutosuggestSplitButton.Location = new Point(764, 156);
            autocompleteAutosuggestSplitButton.Name = "autocompleteAutosuggestSplitButton";
            autocompleteAutosuggestSplitButton.Size = new Size(126, 39);
            autocompleteAutosuggestSplitButton.TabIndex = 47;
            autocompleteAutosuggestSplitButton.Text = "Autocomplete";
            autocompleteAutosuggestSplitButton.UseVisualStyleBackColor = true;
            autocompleteAutosuggestSplitButton.Click += autocompleteAutosuggestSplitButton_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2 });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(174, 52);
            contextMenuStrip1.ItemClicked += contextMenuStrip1_ItemClicked;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(173, 24);
            toolStripMenuItem1.Text = "Autosuggest";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(173, 24);
            toolStripMenuItem2.Text = "Autocomplete";
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
            apiGroupBox.Controls.Add(smartyCheckBox);
            apiGroupBox.Controls.Add(loqateCheckBox);
            apiGroupBox.Controls.Add(googleMapsCheckBox);
            apiGroupBox.Location = new Point(748, 55);
            apiGroupBox.Name = "apiGroupBox";
            apiGroupBox.Size = new Size(344, 68);
            apiGroupBox.TabIndex = 44;
            apiGroupBox.TabStop = false;
            apiGroupBox.Text = "Api";
            // 
            // smartyCheckBox
            // 
            smartyCheckBox.AutoSize = true;
            smartyCheckBox.Location = new Point(261, 26);
            smartyCheckBox.Name = "smartyCheckBox";
            smartyCheckBox.Size = new Size(77, 24);
            smartyCheckBox.TabIndex = 2;
            smartyCheckBox.Text = "Smarty";
            smartyCheckBox.UseVisualStyleBackColor = true;
            smartyCheckBox.CheckedChanged += smartyCheckBox_CheckedChanged;
            // 
            // loqateCheckBox
            // 
            loqateCheckBox.AutoSize = true;
            loqateCheckBox.Location = new Point(153, 26);
            loqateCheckBox.Name = "loqateCheckBox";
            loqateCheckBox.Size = new Size(77, 24);
            loqateCheckBox.TabIndex = 1;
            loqateCheckBox.Text = "Loqate";
            loqateCheckBox.UseVisualStyleBackColor = true;
            loqateCheckBox.CheckedChanged += loqateCheckBox_CheckedChanged;
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
            googleMapsCheckBox.CheckedChanged += googleMapsCheckBox_CheckedChanged;
            // 
            // inputErrorProvider
            // 
            inputErrorProvider.ContainerControl = this;
            // 
            // apiChoiceErrorProvider
            // 
            apiChoiceErrorProvider.ContainerControl = this;
            // 
            // smartyCountryCodeErrorProvider
            // 
            smartyCountryCodeErrorProvider.ContainerControl = this;
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
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)smartyResponseComponetsDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)smartyResponseAnalisisDataGridView).EndInit();
            groupBox1.ResumeLayout(false);
            inputGroupBox.ResumeLayout(false);
            inputGroupBox.PerformLayout();
            inputsChoiceTabControl.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            apiGroupBox.ResumeLayout(false);
            apiGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)inputErrorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)apiChoiceErrorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)smartyCountryCodeErrorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TextBox streetAndHouseNumberTextBox;
        private Label label1;
        private Button checkButton;
        private Label label2;
        private RichTextBox googleResponseOutputTextBox;
        private ComboBox countryCodeComboBox;
        private TextBox cityTextBox;
        private Label label3;
        private TextBox googleResponseFormattedAddressTextBox;
        private Label label4;
        private Label label6;
        private TextBox postalCodeTextBox;
        private Label label7;
        private Label label8;
        private TextBox districtTextBox;
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
        private ErrorProvider inputErrorProvider;
        private ErrorProvider apiChoiceErrorProvider;
        private TextBox googleResponseStreetTextBox;
        private Label label45;
        private TabPage tabPage1;
        private ListBox smartyResponseListBox;
        private CheckBox smartyCheckBox;
        private DataGridView smartyResponseComponetsDataGridView;
        private DataGridView smartyResponseAnalisisDataGridView;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private ErrorProvider smartyCountryCodeErrorProvider;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private Label label47;
        private Label label46;
        private Label label11;
        private Button button4;
        private Controls.SplitButton autocompleteAutosuggestSplitButton;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private TabControl inputsChoiceTabControl;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TextBox freeInputTextBox;
        private Label label10;
    }
}
