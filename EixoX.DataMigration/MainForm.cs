using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EixoX.Data;
using EixoX.Database;

namespace EixoX.DataMigration
{
    public partial class MainForm : Form
    {
        private TreeNode tables;
        private TreeNode views;

        public MainForm()
        {
            InitializeComponent();
            this.tables = this.treeView1.Nodes.Add("Tables");
            this.views = this.treeView1.Nodes.Add("Views");

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 2)
                LoadSourceAndMappings();
        }

        private void Tab1NextBtn_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 1;
        }

        private void BtnTab2Next_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 2;
        }


        private void LoadSourceAndMappings()
        {

            try
            {
                Type sourceType = Assembly.GetAssembly(typeof(Database.Database)).GetType(SourceProviderBox.Text);
                ConstructorInfo constructor = sourceType.GetConstructor(new Type[] { typeof(string) });
                Database.Database sourceDatabase = (Database.Database)constructor.Invoke(new object[] { SourceConnectionStringBox.Text });
                DataTable sourceTables = sourceDatabase.ExecuteTable("SELECT * FROM information_schema.columns WHERE table_schema='public'");
                this.dataGridView1.DataSource = sourceTables;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }
    }
}
