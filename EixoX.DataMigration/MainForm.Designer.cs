namespace EixoX.DataMigration
{
    partial class MainForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.SourceConnectionStringBox = new System.Windows.Forms.TextBox();
            this.SourceConnectionStringLabel = new System.Windows.Forms.Label();
            this.Tab1NextBtn = new System.Windows.Forms.Button();
            this.SourceProviderBox = new System.Windows.Forms.ComboBox();
            this.SourceProviderLabel = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DestinationConnectionStringBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnTab2Next = new System.Windows.Forms.Button();
            this.DestinationProviderBox = new System.Windows.Forms.ComboBox();
            this.DestinationProviderLabel = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SourceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SourceIsIdentity = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SourceLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DestinationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DestinationType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DestinationIsIdentity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.label14 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label13 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1129, 745);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.SourceConnectionStringBox);
            this.tabPage1.Controls.Add(this.SourceConnectionStringLabel);
            this.tabPage1.Controls.Add(this.Tab1NextBtn);
            this.tabPage1.Controls.Add(this.SourceProviderBox);
            this.tabPage1.Controls.Add(this.SourceProviderLabel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1121, 719);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data Source";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // SourceConnectionStringBox
            // 
            this.SourceConnectionStringBox.Location = new System.Drawing.Point(28, 110);
            this.SourceConnectionStringBox.Name = "SourceConnectionStringBox";
            this.SourceConnectionStringBox.Size = new System.Drawing.Size(1072, 20);
            this.SourceConnectionStringBox.TabIndex = 27;
            // 
            // SourceConnectionStringLabel
            // 
            this.SourceConnectionStringLabel.AutoSize = true;
            this.SourceConnectionStringLabel.Location = new System.Drawing.Point(28, 93);
            this.SourceConnectionStringLabel.Name = "SourceConnectionStringLabel";
            this.SourceConnectionStringLabel.Size = new System.Drawing.Size(94, 13);
            this.SourceConnectionStringLabel.TabIndex = 26;
            this.SourceConnectionStringLabel.Text = "Connection String:";
            // 
            // Tab1NextBtn
            // 
            this.Tab1NextBtn.Location = new System.Drawing.Point(478, 191);
            this.Tab1NextBtn.Name = "Tab1NextBtn";
            this.Tab1NextBtn.Size = new System.Drawing.Size(75, 23);
            this.Tab1NextBtn.TabIndex = 25;
            this.Tab1NextBtn.Text = "Next";
            this.Tab1NextBtn.UseVisualStyleBackColor = true;
            this.Tab1NextBtn.Click += new System.EventHandler(this.Tab1NextBtn_Click);
            // 
            // SourceProviderBox
            // 
            this.SourceProviderBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SourceProviderBox.FormattingEnabled = true;
            this.SourceProviderBox.Items.AddRange(new object[] {
            "EixoX.Database.PostgreDatabase",
            "EixoX.Database.MySqlDatabase",
            "EixoX.Database.SqlServerDatabase"});
            this.SourceProviderBox.Location = new System.Drawing.Point(28, 65);
            this.SourceProviderBox.Name = "SourceProviderBox";
            this.SourceProviderBox.Size = new System.Drawing.Size(1072, 21);
            this.SourceProviderBox.TabIndex = 14;
            // 
            // SourceProviderLabel
            // 
            this.SourceProviderLabel.AutoSize = true;
            this.SourceProviderLabel.Location = new System.Drawing.Point(25, 48);
            this.SourceProviderLabel.Name = "SourceProviderLabel";
            this.SourceProviderLabel.Size = new System.Drawing.Size(46, 13);
            this.SourceProviderLabel.TabIndex = 13;
            this.SourceProviderLabel.Text = "Provider";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DestinationConnectionStringBox);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.BtnTab2Next);
            this.tabPage2.Controls.Add(this.DestinationProviderBox);
            this.tabPage2.Controls.Add(this.DestinationProviderLabel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1121, 719);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Data Destination";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DestinationConnectionStringBox
            // 
            this.DestinationConnectionStringBox.Location = new System.Drawing.Point(8, 81);
            this.DestinationConnectionStringBox.Name = "DestinationConnectionStringBox";
            this.DestinationConnectionStringBox.Size = new System.Drawing.Size(1072, 20);
            this.DestinationConnectionStringBox.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Connection String:";
            // 
            // BtnTab2Next
            // 
            this.BtnTab2Next.Location = new System.Drawing.Point(482, 158);
            this.BtnTab2Next.Name = "BtnTab2Next";
            this.BtnTab2Next.Size = new System.Drawing.Size(75, 23);
            this.BtnTab2Next.TabIndex = 30;
            this.BtnTab2Next.Text = "Next";
            this.BtnTab2Next.UseVisualStyleBackColor = true;
            this.BtnTab2Next.Click += new System.EventHandler(this.BtnTab2Next_Click);
            // 
            // DestinationProviderBox
            // 
            this.DestinationProviderBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DestinationProviderBox.FormattingEnabled = true;
            this.DestinationProviderBox.Items.AddRange(new object[] {
            "EixoX.Database.PostgreDatabase",
            "EixoX.Database.MySqlDatabase",
            "EixoX.Database.SqlServerDatabase"});
            this.DestinationProviderBox.Location = new System.Drawing.Point(8, 36);
            this.DestinationProviderBox.Name = "DestinationProviderBox";
            this.DestinationProviderBox.Size = new System.Drawing.Size(1072, 21);
            this.DestinationProviderBox.TabIndex = 29;
            // 
            // DestinationProviderLabel
            // 
            this.DestinationProviderLabel.AutoSize = true;
            this.DestinationProviderLabel.Location = new System.Drawing.Point(5, 19);
            this.DestinationProviderLabel.Name = "DestinationProviderLabel";
            this.DestinationProviderLabel.Size = new System.Drawing.Size(46, 13);
            this.DestinationProviderLabel.TabIndex = 28;
            this.DestinationProviderLabel.Text = "Provider";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1121, 719);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Mappings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(1121, 719);
            this.splitContainer1.SplitterDistance = 373;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(373, 719);
            this.treeView1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SourceName,
            this.SourceType,
            this.SourceIsIdentity,
            this.SourceLength,
            this.DestinationName,
            this.DestinationType,
            this.DestinationIsIdentity});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(744, 719);
            this.dataGridView1.TabIndex = 0;
            // 
            // SourceName
            // 
            this.SourceName.HeaderText = "SourceName";
            this.SourceName.Name = "SourceName";
            // 
            // SourceType
            // 
            this.SourceType.HeaderText = "SourceType";
            this.SourceType.Name = "SourceType";
            // 
            // SourceIsIdentity
            // 
            this.SourceIsIdentity.HeaderText = "Is Identity";
            this.SourceIsIdentity.Name = "SourceIsIdentity";
            // 
            // SourceLength
            // 
            this.SourceLength.HeaderText = "Length";
            this.SourceLength.Name = "SourceLength";
            // 
            // DestinationName
            // 
            this.DestinationName.HeaderText = "Destination Name";
            this.DestinationName.Name = "DestinationName";
            // 
            // DestinationType
            // 
            this.DestinationType.HeaderText = "Type";
            this.DestinationType.Name = "DestinationType";
            // 
            // DestinationIsIdentity
            // 
            this.DestinationIsIdentity.HeaderText = "Is Identity";
            this.DestinationIsIdentity.Name = "DestinationIsIdentity";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.button3);
            this.tabPage4.Controls.Add(this.listView1);
            this.tabPage4.Controls.Add(this.progressBar2);
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Controls.Add(this.progressBar1);
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1121, 719);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Execute";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(453, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(27, 137);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(501, 135);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(27, 107);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(501, 23);
            this.progressBar2.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(27, 90);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 13);
            this.label14.TabIndex = 2;
            this.label14.Text = "Row";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(27, 43);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(501, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(24, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Table";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 745);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button Tab1NextBtn;
        private System.Windows.Forms.ComboBox SourceProviderBox;
        private System.Windows.Forms.Label SourceProviderLabel;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceName;
        private System.Windows.Forms.DataGridViewComboBoxColumn SourceType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SourceIsIdentity;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestinationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestinationType;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestinationIsIdentity;
        private System.Windows.Forms.Label SourceConnectionStringLabel;
        private System.Windows.Forms.TextBox SourceConnectionStringBox;
        private System.Windows.Forms.TextBox DestinationConnectionStringBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnTab2Next;
        private System.Windows.Forms.ComboBox DestinationProviderBox;
        private System.Windows.Forms.Label DestinationProviderLabel;
    }
}

