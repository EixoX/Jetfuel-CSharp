using EixoX.Restrictions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RocketLauncherDBCode
{
    public partial class ConfigurationForm : Form
    {
        [Required]
        public string ConnectionString
        {
            get { return this.txtConnectionString.Text; }
            set { this.txtConnectionString.Text = value; }
        }

        [Required]
        public string Directory
        {
            get { return this.txtDirectory.Text; }
            set { this.txtDirectory.Text = value; }
        }

        public ConfigurationForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!RestrictionAspect<ConfigurationForm>.Instance.Validate(this))
            {
                MessageBox.Show("Please, check the informations");
                return;
            }

            RocketLauncherManager.Instance.ConnectionString = this.ConnectionString;
            RocketLauncherManager.Instance.Directory = this.Directory;

            MessageBox.Show("Saved! ;)");
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.Directory = dialog.SelectedPath;
            }
        }
    }
}
