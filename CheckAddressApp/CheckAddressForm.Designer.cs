using qAcProviderTest;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckAddressForm));
            streetAndHouseNumberTextBox = new TextBox();
            label1 = new Label();
            checkButton = new Button();
            cityTextBox = new TextBox();
            label3 = new Label();
            label6 = new Label();
            postalCodeTextBox = new TextBox();
            label7 = new Label();
            label8 = new Label();
            districtTextBox = new TextBox();
            button1 = new Button();
            button2 = new Button();
            apiTabControl = new TabControl();
            googleMapsTabPage = new TabPage();
            googleResponseTimeLabel = new Label();
            label2 = new Label();
            googleResponseListBox = new ListBox();
            label4 = new Label();
            googleResponseDataGridView = new DataGridView();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            loqateTabPage = new TabPage();
            loqateResponseTimeLabel = new Label();
            button3 = new Button();
            label18 = new Label();
            loqateResponseDataGridView = new DataGridView();
            fieldColumn = new DataGridViewTextBoxColumn();
            valueColumn = new DataGridViewTextBoxColumn();
            label43 = new Label();
            loqateResponseListBox = new ListBox();
            smartyTabPage = new TabPage();
            smartyResponseTimeLabel = new Label();
            smartyResponseDataGridView = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            button4 = new Button();
            label47 = new Label();
            label11 = new Label();
            smartyResponseListBox = new ListBox();
            hereTabPage = new TabPage();
            hereResponseTimeLabel = new Label();
            button5 = new Button();
            label48 = new Label();
            hereResponseListBox = new ListBox();
            label20 = new Label();
            hereResponseDataGridView = new DataGridView();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            groupBox1 = new GroupBox();
            inputGroupBox = new GroupBox();
            addressesFileNameTextBox = new TextBox();
            label5 = new Label();
            addressesSearchTextBox = new TextBox();
            loadAddressesButton = new Button();
            saveAsAddressesButton = new Button();
            insertAddressButton = new Button();
            updateAddressButton = new Button();
            deleteAddressButton = new Button();
            autosuggestButton = new Button();
            autocompleteButton = new Button();
            addressesListBoxLabel = new Label();
            addressesFromFileListBox = new ListBoxWithScrollEvent();
            countryTextBox = new TextBox();
            inputsChoiceTabControl = new TabControl();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            freeInputTextBox = new TextBox();
            label10 = new Label();
            label9 = new Label();
            requestAddressTextBox = new TextBox();
            apiGroupBox = new GroupBox();
            hereCheckBox = new CheckBox();
            smartyCheckBox = new CheckBox();
            loqateCheckBox = new CheckBox();
            googleMapsCheckBox = new CheckBox();
            saveAsAddressesFileDialog = new SaveFileDialog();
            openAddressesFileDialog = new OpenFileDialog();
            apiTabControl.SuspendLayout();
            googleMapsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)googleResponseDataGridView).BeginInit();
            loqateTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)loqateResponseDataGridView).BeginInit();
            smartyTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)smartyResponseDataGridView).BeginInit();
            hereTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)hereResponseDataGridView).BeginInit();
            groupBox1.SuspendLayout();
            inputGroupBox.SuspendLayout();
            inputsChoiceTabControl.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            apiGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // streetAndHouseNumberTextBox
            // 
            streetAndHouseNumberTextBox.Location = new Point(6, 40);
            streetAndHouseNumberTextBox.Name = "streetAndHouseNumberTextBox";
            streetAndHouseNumberTextBox.Size = new Size(258, 27);
            streetAndHouseNumberTextBox.TabIndex = 0;
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
            checkButton.Location = new Point(253, 300);
            checkButton.Name = "checkButton";
            checkButton.Size = new Size(95, 39);
            checkButton.TabIndex = 2;
            checkButton.Text = "Check";
            checkButton.UseVisualStyleBackColor = true;
            checkButton.Click += checkButton_Click;
            // 
            // cityTextBox
            // 
            cityTextBox.Location = new Point(6, 99);
            cityTextBox.Name = "cityTextBox";
            cityTextBox.Size = new Size(189, 27);
            cityTextBox.TabIndex = 7;
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
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(270, 17);
            label6.Name = "label6";
            label6.Size = new Size(87, 20);
            label6.TabIndex = 14;
            label6.Text = "Postal Code";
            // 
            // postalCodeTextBox
            // 
            postalCodeTextBox.Location = new Point(270, 40);
            postalCodeTextBox.Name = "postalCodeTextBox";
            postalCodeTextBox.Size = new Size(113, 27);
            postalCodeTextBox.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(415, 72);
            label7.Name = "label7";
            label7.Size = new Size(60, 20);
            label7.TabIndex = 15;
            label7.Text = "Country";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(201, 75);
            label8.Name = "label8";
            label8.Size = new Size(56, 20);
            label8.TabIndex = 17;
            label8.Text = "District";
            // 
            // districtTextBox
            // 
            districtTextBox.Location = new Point(201, 99);
            districtTextBox.Name = "districtTextBox";
            districtTextBox.Size = new Size(182, 27);
            districtTextBox.TabIndex = 16;
            // 
            // button1
            // 
            button1.Location = new Point(382, 403);
            button1.Name = "button1";
            button1.Size = new Size(94, 39);
            button1.TabIndex = 41;
            button1.Text = "Clear";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(354, 300);
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
            apiTabControl.Controls.Add(smartyTabPage);
            apiTabControl.Controls.Add(hereTabPage);
            apiTabControl.Location = new Point(16, 26);
            apiTabControl.Name = "apiTabControl";
            apiTabControl.SelectedIndex = 0;
            apiTabControl.Size = new Size(1223, 485);
            apiTabControl.TabIndex = 43;
            // 
            // googleMapsTabPage
            // 
            googleMapsTabPage.Controls.Add(googleResponseTimeLabel);
            googleMapsTabPage.Controls.Add(label2);
            googleMapsTabPage.Controls.Add(googleResponseListBox);
            googleMapsTabPage.Controls.Add(label4);
            googleMapsTabPage.Controls.Add(googleResponseDataGridView);
            googleMapsTabPage.Controls.Add(button1);
            googleMapsTabPage.Location = new Point(4, 29);
            googleMapsTabPage.Name = "googleMapsTabPage";
            googleMapsTabPage.Padding = new Padding(3);
            googleMapsTabPage.Size = new Size(1215, 452);
            googleMapsTabPage.TabIndex = 0;
            googleMapsTabPage.Text = "Google Maps";
            googleMapsTabPage.UseVisualStyleBackColor = true;
            // 
            // googleResponseTimeLabel
            // 
            googleResponseTimeLabel.AutoSize = true;
            googleResponseTimeLabel.Location = new Point(6, 3);
            googleResponseTimeLabel.Name = "googleResponseTimeLabel";
            googleResponseTimeLabel.Size = new Size(138, 20);
            googleResponseTimeLabel.TabIndex = 90;
            googleResponseTimeLabel.Text = "Response time: 0,0s";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 38);
            label2.Name = "label2";
            label2.Size = new Size(79, 20);
            label2.TabIndex = 89;
            label2.Text = "Result Hits";
            // 
            // googleResponseListBox
            // 
            googleResponseListBox.FormattingEnabled = true;
            googleResponseListBox.HorizontalScrollbar = true;
            googleResponseListBox.Location = new Point(6, 61);
            googleResponseListBox.Name = "googleResponseListBox";
            googleResponseListBox.Size = new Size(470, 144);
            googleResponseListBox.TabIndex = 88;
            googleResponseListBox.SelectedIndexChanged += googleResponseListBox_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(482, 38);
            label4.Name = "label4";
            label4.Size = new Size(79, 20);
            label4.TabIndex = 87;
            label4.Text = "Hit Details";
            // 
            // googleResponseDataGridView
            // 
            googleResponseDataGridView.AllowUserToAddRows = false;
            googleResponseDataGridView.AllowUserToDeleteRows = false;
            googleResponseDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            googleResponseDataGridView.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn7, dataGridViewTextBoxColumn8 });
            googleResponseDataGridView.Location = new Point(482, 61);
            googleResponseDataGridView.Name = "googleResponseDataGridView";
            googleResponseDataGridView.ReadOnly = true;
            googleResponseDataGridView.RowHeadersVisible = false;
            googleResponseDataGridView.RowHeadersWidth = 51;
            googleResponseDataGridView.RowTemplate.Height = 25;
            googleResponseDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            googleResponseDataGridView.Size = new Size(700, 381);
            googleResponseDataGridView.TabIndex = 86;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.HeaderText = "Field";
            dataGridViewTextBoxColumn7.MinimumWidth = 6;
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            dataGridViewTextBoxColumn7.ReadOnly = true;
            dataGridViewTextBoxColumn7.Width = 350;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewTextBoxColumn8.HeaderText = "Value";
            dataGridViewTextBoxColumn8.MinimumWidth = 6;
            dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            dataGridViewTextBoxColumn8.ReadOnly = true;
            dataGridViewTextBoxColumn8.Width = 350;
            // 
            // loqateTabPage
            // 
            loqateTabPage.Controls.Add(loqateResponseTimeLabel);
            loqateTabPage.Controls.Add(button3);
            loqateTabPage.Controls.Add(label18);
            loqateTabPage.Controls.Add(loqateResponseDataGridView);
            loqateTabPage.Controls.Add(label43);
            loqateTabPage.Controls.Add(loqateResponseListBox);
            loqateTabPage.Location = new Point(4, 29);
            loqateTabPage.Name = "loqateTabPage";
            loqateTabPage.Padding = new Padding(3);
            loqateTabPage.Size = new Size(1215, 452);
            loqateTabPage.TabIndex = 1;
            loqateTabPage.Text = "Loqate";
            loqateTabPage.UseVisualStyleBackColor = true;
            // 
            // loqateResponseTimeLabel
            // 
            loqateResponseTimeLabel.AutoSize = true;
            loqateResponseTimeLabel.Location = new Point(6, 3);
            loqateResponseTimeLabel.Name = "loqateResponseTimeLabel";
            loqateResponseTimeLabel.Size = new Size(138, 20);
            loqateResponseTimeLabel.TabIndex = 91;
            loqateResponseTimeLabel.Text = "Response time: 0,0s";
            // 
            // button3
            // 
            button3.Location = new Point(382, 403);
            button3.Name = "button3";
            button3.Size = new Size(94, 39);
            button3.TabIndex = 82;
            button3.Text = "Clear";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(482, 38);
            label18.Name = "label18";
            label18.Size = new Size(79, 20);
            label18.TabIndex = 79;
            label18.Text = "Hit Details";
            // 
            // loqateResponseDataGridView
            // 
            loqateResponseDataGridView.AllowUserToAddRows = false;
            loqateResponseDataGridView.AllowUserToDeleteRows = false;
            loqateResponseDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            loqateResponseDataGridView.Columns.AddRange(new DataGridViewColumn[] { fieldColumn, valueColumn });
            loqateResponseDataGridView.Location = new Point(482, 61);
            loqateResponseDataGridView.Name = "loqateResponseDataGridView";
            loqateResponseDataGridView.ReadOnly = true;
            loqateResponseDataGridView.RowHeadersVisible = false;
            loqateResponseDataGridView.RowHeadersWidth = 51;
            loqateResponseDataGridView.RowTemplate.Height = 25;
            loqateResponseDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            loqateResponseDataGridView.Size = new Size(700, 381);
            loqateResponseDataGridView.TabIndex = 78;
            // 
            // fieldColumn
            // 
            fieldColumn.HeaderText = "Field";
            fieldColumn.MinimumWidth = 6;
            fieldColumn.Name = "fieldColumn";
            fieldColumn.ReadOnly = true;
            fieldColumn.Width = 350;
            // 
            // valueColumn
            // 
            valueColumn.HeaderText = "Value";
            valueColumn.MinimumWidth = 6;
            valueColumn.Name = "valueColumn";
            valueColumn.ReadOnly = true;
            valueColumn.Width = 350;
            // 
            // label43
            // 
            label43.AutoSize = true;
            label43.Location = new Point(6, 38);
            label43.Name = "label43";
            label43.Size = new Size(79, 20);
            label43.TabIndex = 75;
            label43.Text = "Result Hits";
            // 
            // loqateResponseListBox
            // 
            loqateResponseListBox.FormattingEnabled = true;
            loqateResponseListBox.HorizontalScrollbar = true;
            loqateResponseListBox.Location = new Point(6, 61);
            loqateResponseListBox.Name = "loqateResponseListBox";
            loqateResponseListBox.Size = new Size(470, 144);
            loqateResponseListBox.TabIndex = 25;
            loqateResponseListBox.SelectedIndexChanged += loqateResponseListBox_SelectedIndexChanged;
            // 
            // smartyTabPage
            // 
            smartyTabPage.Controls.Add(smartyResponseTimeLabel);
            smartyTabPage.Controls.Add(smartyResponseDataGridView);
            smartyTabPage.Controls.Add(button4);
            smartyTabPage.Controls.Add(label47);
            smartyTabPage.Controls.Add(label11);
            smartyTabPage.Controls.Add(smartyResponseListBox);
            smartyTabPage.Location = new Point(4, 29);
            smartyTabPage.Name = "smartyTabPage";
            smartyTabPage.Padding = new Padding(3);
            smartyTabPage.Size = new Size(1215, 452);
            smartyTabPage.TabIndex = 2;
            smartyTabPage.Text = "Smarty";
            smartyTabPage.UseVisualStyleBackColor = true;
            // 
            // smartyResponseTimeLabel
            // 
            smartyResponseTimeLabel.AutoSize = true;
            smartyResponseTimeLabel.Location = new Point(6, 3);
            smartyResponseTimeLabel.Name = "smartyResponseTimeLabel";
            smartyResponseTimeLabel.Size = new Size(138, 20);
            smartyResponseTimeLabel.TabIndex = 91;
            smartyResponseTimeLabel.Text = "Response time: 0,0s";
            // 
            // smartyResponseDataGridView
            // 
            smartyResponseDataGridView.AllowUserToAddRows = false;
            smartyResponseDataGridView.AllowUserToDeleteRows = false;
            smartyResponseDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            smartyResponseDataGridView.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2 });
            smartyResponseDataGridView.Location = new Point(482, 61);
            smartyResponseDataGridView.Name = "smartyResponseDataGridView";
            smartyResponseDataGridView.ReadOnly = true;
            smartyResponseDataGridView.RowHeadersVisible = false;
            smartyResponseDataGridView.RowHeadersWidth = 51;
            smartyResponseDataGridView.RowTemplate.Height = 25;
            smartyResponseDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            smartyResponseDataGridView.Size = new Size(700, 381);
            smartyResponseDataGridView.TabIndex = 85;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Field";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.Width = 350;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Value";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            dataGridViewTextBoxColumn2.Width = 350;
            // 
            // button4
            // 
            button4.Location = new Point(382, 403);
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
            label47.Location = new Point(482, 38);
            label47.Name = "label47";
            label47.Size = new Size(79, 20);
            label47.TabIndex = 83;
            label47.Text = "Hit Details";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 38);
            label11.Name = "label11";
            label11.Size = new Size(79, 20);
            label11.TabIndex = 81;
            label11.Text = "Result Hits";
            // 
            // smartyResponseListBox
            // 
            smartyResponseListBox.FormattingEnabled = true;
            smartyResponseListBox.HorizontalScrollbar = true;
            smartyResponseListBox.Location = new Point(6, 61);
            smartyResponseListBox.Name = "smartyResponseListBox";
            smartyResponseListBox.Size = new Size(470, 144);
            smartyResponseListBox.TabIndex = 26;
            smartyResponseListBox.SelectedIndexChanged += smartyResponseListBox_SelectedIndexChanged;
            // 
            // hereTabPage
            // 
            hereTabPage.Controls.Add(hereResponseTimeLabel);
            hereTabPage.Controls.Add(button5);
            hereTabPage.Controls.Add(label48);
            hereTabPage.Controls.Add(hereResponseListBox);
            hereTabPage.Controls.Add(label20);
            hereTabPage.Controls.Add(hereResponseDataGridView);
            hereTabPage.Location = new Point(4, 29);
            hereTabPage.Name = "hereTabPage";
            hereTabPage.Size = new Size(1215, 452);
            hereTabPage.TabIndex = 3;
            hereTabPage.Text = "Here";
            hereTabPage.UseVisualStyleBackColor = true;
            // 
            // hereResponseTimeLabel
            // 
            hereResponseTimeLabel.AutoSize = true;
            hereResponseTimeLabel.Location = new Point(6, 3);
            hereResponseTimeLabel.Name = "hereResponseTimeLabel";
            hereResponseTimeLabel.Size = new Size(138, 20);
            hereResponseTimeLabel.TabIndex = 91;
            hereResponseTimeLabel.Text = "Response time: 0,0s";
            // 
            // button5
            // 
            button5.Location = new Point(382, 403);
            button5.Name = "button5";
            button5.Size = new Size(94, 39);
            button5.TabIndex = 85;
            button5.Text = "Clear";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label48
            // 
            label48.AutoSize = true;
            label48.Location = new Point(6, 38);
            label48.Name = "label48";
            label48.Size = new Size(79, 20);
            label48.TabIndex = 83;
            label48.Text = "Result Hits";
            // 
            // hereResponseListBox
            // 
            hereResponseListBox.FormattingEnabled = true;
            hereResponseListBox.HorizontalScrollbar = true;
            hereResponseListBox.Location = new Point(6, 61);
            hereResponseListBox.Name = "hereResponseListBox";
            hereResponseListBox.Size = new Size(470, 144);
            hereResponseListBox.TabIndex = 82;
            hereResponseListBox.SelectedIndexChanged += hereResponseListBox_SelectedIndexChanged;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(482, 38);
            label20.Name = "label20";
            label20.Size = new Size(79, 20);
            label20.TabIndex = 81;
            label20.Text = "Hit Details";
            // 
            // hereResponseDataGridView
            // 
            hereResponseDataGridView.AllowUserToAddRows = false;
            hereResponseDataGridView.AllowUserToDeleteRows = false;
            hereResponseDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            hereResponseDataGridView.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6 });
            hereResponseDataGridView.Location = new Point(482, 61);
            hereResponseDataGridView.Name = "hereResponseDataGridView";
            hereResponseDataGridView.ReadOnly = true;
            hereResponseDataGridView.RowHeadersVisible = false;
            hereResponseDataGridView.RowHeadersWidth = 51;
            hereResponseDataGridView.RowTemplate.Height = 25;
            hereResponseDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            hereResponseDataGridView.Size = new Size(700, 381);
            hereResponseDataGridView.TabIndex = 80;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Field";
            dataGridViewTextBoxColumn5.MinimumWidth = 6;
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            dataGridViewTextBoxColumn5.Width = 350;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.HeaderText = "Value";
            dataGridViewTextBoxColumn6.MinimumWidth = 6;
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            dataGridViewTextBoxColumn6.Width = 350;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(apiTabControl);
            groupBox1.Location = new Point(12, 459);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1279, 521);
            groupBox1.TabIndex = 44;
            groupBox1.TabStop = false;
            groupBox1.Text = "Response";
            // 
            // inputGroupBox
            // 
            inputGroupBox.Controls.Add(addressesFileNameTextBox);
            inputGroupBox.Controls.Add(label5);
            inputGroupBox.Controls.Add(addressesSearchTextBox);
            inputGroupBox.Controls.Add(loadAddressesButton);
            inputGroupBox.Controls.Add(saveAsAddressesButton);
            inputGroupBox.Controls.Add(insertAddressButton);
            inputGroupBox.Controls.Add(updateAddressButton);
            inputGroupBox.Controls.Add(deleteAddressButton);
            inputGroupBox.Controls.Add(autosuggestButton);
            inputGroupBox.Controls.Add(autocompleteButton);
            inputGroupBox.Controls.Add(addressesListBoxLabel);
            inputGroupBox.Controls.Add(addressesFromFileListBox);
            inputGroupBox.Controls.Add(countryTextBox);
            inputGroupBox.Controls.Add(inputsChoiceTabControl);
            inputGroupBox.Controls.Add(label9);
            inputGroupBox.Controls.Add(requestAddressTextBox);
            inputGroupBox.Controls.Add(apiGroupBox);
            inputGroupBox.Controls.Add(button2);
            inputGroupBox.Controls.Add(checkButton);
            inputGroupBox.Controls.Add(label7);
            inputGroupBox.Location = new Point(12, 12);
            inputGroupBox.Name = "inputGroupBox";
            inputGroupBox.Size = new Size(1279, 441);
            inputGroupBox.TabIndex = 45;
            inputGroupBox.TabStop = false;
            inputGroupBox.Text = "Input";
            // 
            // addressesFileNameTextBox
            // 
            addressesFileNameTextBox.Location = new Point(738, 22);
            addressesFileNameTextBox.Name = "addressesFileNameTextBox";
            addressesFileNameTextBox.ReadOnly = true;
            addressesFileNameTextBox.Size = new Size(245, 27);
            addressesFileNameTextBox.TabIndex = 101;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1015, 25);
            label5.Name = "label5";
            label5.Size = new Size(53, 20);
            label5.TabIndex = 100;
            label5.Text = "Search";
            // 
            // addressesSearchTextBox
            // 
            addressesSearchTextBox.Location = new Point(1074, 22);
            addressesSearchTextBox.Name = "addressesSearchTextBox";
            addressesSearchTextBox.Size = new Size(165, 27);
            addressesSearchTextBox.TabIndex = 99;
            addressesSearchTextBox.TextChanged += addressesSearchTextBox_TextChanged;
            // 
            // loadAddressesButton
            // 
            loadAddressesButton.Location = new Point(794, 305);
            loadAddressesButton.Name = "loadAddressesButton";
            loadAddressesButton.Size = new Size(95, 39);
            loadAddressesButton.TabIndex = 98;
            loadAddressesButton.Text = "Load";
            loadAddressesButton.UseVisualStyleBackColor = true;
            loadAddressesButton.Click += loadAddressesButton_Click;
            // 
            // saveAsAddressesButton
            // 
            saveAsAddressesButton.Location = new Point(693, 305);
            saveAsAddressesButton.Name = "saveAsAddressesButton";
            saveAsAddressesButton.Size = new Size(95, 39);
            saveAsAddressesButton.TabIndex = 97;
            saveAsAddressesButton.Text = "Save as";
            saveAsAddressesButton.UseVisualStyleBackColor = true;
            saveAsAddressesButton.Click += saveAsAddressesButton_Click;
            // 
            // insertAddressButton
            // 
            insertAddressButton.Location = new Point(592, 55);
            insertAddressButton.Name = "insertAddressButton";
            insertAddressButton.Size = new Size(95, 39);
            insertAddressButton.TabIndex = 96;
            insertAddressButton.Text = "Insert";
            insertAddressButton.UseVisualStyleBackColor = true;
            insertAddressButton.Click += insertAddressButton_Click;
            // 
            // updateAddressButton
            // 
            updateAddressButton.Location = new Point(592, 100);
            updateAddressButton.Name = "updateAddressButton";
            updateAddressButton.Size = new Size(95, 39);
            updateAddressButton.TabIndex = 95;
            updateAddressButton.Text = "Update";
            updateAddressButton.UseVisualStyleBackColor = true;
            updateAddressButton.Click += updateAddressButton_Click;
            // 
            // deleteAddressButton
            // 
            deleteAddressButton.Location = new Point(592, 145);
            deleteAddressButton.Name = "deleteAddressButton";
            deleteAddressButton.Size = new Size(95, 39);
            deleteAddressButton.TabIndex = 94;
            deleteAddressButton.Text = "Delete";
            deleteAddressButton.UseVisualStyleBackColor = true;
            deleteAddressButton.Click += deleteAddressButton_Click;
            // 
            // autosuggestButton
            // 
            autosuggestButton.Location = new Point(139, 300);
            autosuggestButton.Name = "autosuggestButton";
            autosuggestButton.Size = new Size(108, 39);
            autosuggestButton.TabIndex = 93;
            autosuggestButton.Text = "Autosuggest";
            autosuggestButton.UseVisualStyleBackColor = true;
            autosuggestButton.Click += autosuggestButton_Click;
            // 
            // autocompleteButton
            // 
            autocompleteButton.Location = new Point(16, 300);
            autocompleteButton.Name = "autocompleteButton";
            autocompleteButton.Size = new Size(117, 39);
            autocompleteButton.TabIndex = 92;
            autocompleteButton.Text = "Autocomplete";
            autocompleteButton.UseVisualStyleBackColor = true;
            autocompleteButton.Click += autocompleteButton_Click;
            // 
            // addressesListBoxLabel
            // 
            addressesListBoxLabel.AutoSize = true;
            addressesListBoxLabel.Location = new Point(593, 25);
            addressesListBoxLabel.Name = "addressesListBoxLabel";
            addressesListBoxLabel.Size = new Size(142, 20);
            addressesListBoxLabel.TabIndex = 91;
            addressesListBoxLabel.Text = "Addresses from File:";
            // 
            // addressesFromFileListBox
            // 
            addressesFromFileListBox.FormattingEnabled = true;
            addressesFromFileListBox.Location = new Point(693, 55);
            addressesFromFileListBox.Name = "addressesFromFileListBox";
            addressesFromFileListBox.Size = new Size(546, 244);
            addressesFromFileListBox.TabIndex = 90;
            addressesFromFileListBox.SelectedIndexChanged += fromFileInputAddresseslistBox_SelectedIndexChanged;
            // 
            // countryTextBox
            // 
            countryTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            countryTextBox.Location = new Point(416, 95);
            countryTextBox.Name = "countryTextBox";
            countryTextBox.Size = new Size(125, 27);
            countryTextBox.TabIndex = 49;
            // 
            // inputsChoiceTabControl
            // 
            inputsChoiceTabControl.Controls.Add(tabPage2);
            inputsChoiceTabControl.Controls.Add(tabPage3);
            inputsChoiceTabControl.Location = new Point(16, 26);
            inputsChoiceTabControl.Name = "inputsChoiceTabControl";
            inputsChoiceTabControl.SelectedIndex = 0;
            inputsChoiceTabControl.Size = new Size(398, 169);
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
            tabPage2.Size = new Size(390, 136);
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
            tabPage3.Size = new Size(390, 136);
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
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(16, 376);
            label9.Name = "label9";
            label9.Size = new Size(117, 20);
            label9.TabIndex = 46;
            label9.Text = "Request address";
            // 
            // requestAddressTextBox
            // 
            requestAddressTextBox.Location = new Point(16, 399);
            requestAddressTextBox.Name = "requestAddressTextBox";
            requestAddressTextBox.ReadOnly = true;
            requestAddressTextBox.Size = new Size(1223, 27);
            requestAddressTextBox.TabIndex = 45;
            // 
            // apiGroupBox
            // 
            apiGroupBox.Controls.Add(hereCheckBox);
            apiGroupBox.Controls.Add(smartyCheckBox);
            apiGroupBox.Controls.Add(loqateCheckBox);
            apiGroupBox.Controls.Add(googleMapsCheckBox);
            apiGroupBox.Location = new Point(16, 207);
            apiGroupBox.Name = "apiGroupBox";
            apiGroupBox.Size = new Size(398, 68);
            apiGroupBox.TabIndex = 44;
            apiGroupBox.TabStop = false;
            apiGroupBox.Text = "Check Provider";
            // 
            // hereCheckBox
            // 
            hereCheckBox.AutoSize = true;
            hereCheckBox.Location = new Point(6, 26);
            hereCheckBox.Name = "hereCheckBox";
            hereCheckBox.Size = new Size(67, 24);
            hereCheckBox.TabIndex = 3;
            hereCheckBox.Text = "HERE";
            hereCheckBox.UseVisualStyleBackColor = true;
            hereCheckBox.CheckedChanged += hereCheckBox_CheckedChanged;
            // 
            // smartyCheckBox
            // 
            smartyCheckBox.AutoSize = true;
            smartyCheckBox.Location = new Point(79, 26);
            smartyCheckBox.Name = "smartyCheckBox";
            smartyCheckBox.Size = new Size(86, 24);
            smartyCheckBox.TabIndex = 2;
            smartyCheckBox.Text = "SMARTY";
            smartyCheckBox.UseVisualStyleBackColor = true;
            smartyCheckBox.CheckedChanged += smartyCheckBox_CheckedChanged;
            // 
            // loqateCheckBox
            // 
            loqateCheckBox.AutoSize = true;
            loqateCheckBox.Location = new Point(308, 26);
            loqateCheckBox.Name = "loqateCheckBox";
            loqateCheckBox.Size = new Size(84, 24);
            loqateCheckBox.TabIndex = 1;
            loqateCheckBox.Text = "LOQATE";
            loqateCheckBox.UseVisualStyleBackColor = true;
            loqateCheckBox.CheckedChanged += loqateCheckBox_CheckedChanged;
            // 
            // googleMapsCheckBox
            // 
            googleMapsCheckBox.AutoSize = true;
            googleMapsCheckBox.Location = new Point(171, 26);
            googleMapsCheckBox.Name = "googleMapsCheckBox";
            googleMapsCheckBox.Size = new Size(131, 24);
            googleMapsCheckBox.TabIndex = 0;
            googleMapsCheckBox.Text = "GOOGLE MAPS";
            googleMapsCheckBox.UseVisualStyleBackColor = true;
            googleMapsCheckBox.CheckedChanged += googleMapsCheckBox_CheckedChanged;
            // 
            // saveAsAddressesFileDialog
            // 
            saveAsAddressesFileDialog.Filter = "*.csv | *.csv";
            saveAsAddressesFileDialog.OverwritePrompt = false;
            // 
            // openAddressesFileDialog
            // 
            openAddressesFileDialog.FileName = "openFileDialog1";
            openAddressesFileDialog.Filter = "*csv | *csv";
            // 
            // CheckAddressForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1318, 999);
            Controls.Add(inputGroupBox);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CheckAddressForm";
            Text = "q.address AC Provider Test";
            FormClosing += CheckAddressForm_FormClosing;
            Load += CheckAddressForm_Load;
            apiTabControl.ResumeLayout(false);
            googleMapsTabPage.ResumeLayout(false);
            googleMapsTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)googleResponseDataGridView).EndInit();
            loqateTabPage.ResumeLayout(false);
            loqateTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)loqateResponseDataGridView).EndInit();
            smartyTabPage.ResumeLayout(false);
            smartyTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)smartyResponseDataGridView).EndInit();
            hereTabPage.ResumeLayout(false);
            hereTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)hereResponseDataGridView).EndInit();
            groupBox1.ResumeLayout(false);
            inputGroupBox.ResumeLayout(false);
            inputGroupBox.PerformLayout();
            inputsChoiceTabControl.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            apiGroupBox.ResumeLayout(false);
            apiGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox streetAndHouseNumberTextBox;
        private Label label1;
        private Button checkButton;
        private TextBox cityTextBox;
        private Label label3;
        private Label label6;
        private TextBox postalCodeTextBox;
        private Label label7;
        private Label label8;
        private TextBox districtTextBox;
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
        private Label label20;
        private DataGridView loqateResponseDataGridView;
        private Label label18;
        private Button button3;
        private TextBox requestAddressTextBox;
        private Label label9;
        private TabPage smartyTabPage;
        private ListBox smartyResponseListBox;
        private CheckBox smartyCheckBox;
        private Label label47;
        private Label label11;
        private Button button4;
        private TabControl inputsChoiceTabControl;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TextBox freeInputTextBox;
        private Label label10;
        private CheckBox hereCheckBox;
        private TabPage hereTabPage;
        private Label label48;
        private ListBox hereResponseListBox;
        private Button button5;
        private TextBox countryTextBox;
        private Label label2;
        private ListBox googleResponseListBox;
        private Label label4;
        private DataGridView googleResponseDataGridView;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn fieldColumn;
        private DataGridViewTextBoxColumn valueColumn;
        private DataGridView smartyResponseDataGridView;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridView hereResponseDataGridView;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private Label addressesListBoxLabel;
        private ListBoxWithScrollEvent addressesFromFileListBox;
        private SaveFileDialog saveAsAddressesFileDialog;
        private Button autosuggestButton;
        private Button autocompleteButton;
        private Button loadAddressesButton;
        private Button saveAsAddressesButton;
        private Button insertAddressButton;
        private Button updateAddressButton;
        private Button deleteAddressButton;
        private OpenFileDialog openAddressesFileDialog;
        private TextBox addressesSearchTextBox;
        private Label label5;
        private TextBox addressesFileNameTextBox;
        private Label googleResponseTimeLabel;
        private Label loqateResponseTimeLabel;
        private Label smartyResponseTimeLabel;
        private Label hereResponseTimeLabel;
    }
}
