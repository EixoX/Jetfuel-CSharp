using EixoX;
using EixoX.RocketLauncher;
using EixoX.RocketLauncher.Command;
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
    public partial class Form1 : Form, RocketLauncherViewee, IRocketLauncherView
    {
        public Form1()
        {
            InitializeComponent();
            RocketLauncherManager.Instance.WatchConnectionString(this.lblDatabase);
            RocketLauncherManager.Instance.WatchDirectory(this.lblDirectory);
            RocketLauncherManager.Instance.Viewee = this;
        }

        private void btnConfiguration_Click(object sender, EventArgs e)
        {
            ConfigurationForm configForm = new ConfigurationForm();
            configForm.ShowDialog();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            RocketLauncherManager.Instance.Connect(this.gridDbTables);
        }

        bool RocketLauncherViewee.YesOrNo(string message)
        {
            throw new NotImplementedException();
        }

        void Viewee.OnException(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        private void btnCreateFile_Click(object sender, EventArgs e)
        {
            ClassesFromDatabaseCommand command = new ClassesFromDatabaseCommand(
                EixoX.RocketLauncher.ProgrammingLanguage.Csharp,
                RocketLauncherManager.Instance.Directory,
                this);

            command.Run(true, RocketLauncherManager.Instance.ConnectionString);
        }

        public Commands GetMenuCommand()
        {
            throw new NotImplementedException();
        }

        public ProgrammingLanguage GetProgrammingLanguage()
        {
            return ProgrammingLanguage.Csharp;
        }

        public string GetDirectory()
        {
            return RocketLauncherManager.Instance.Directory;
        }

        public void ShowCommandMenu()
        {
            throw new NotImplementedException();
        }

        public void ShowWelcomeMessage()
        {
            throw new NotImplementedException();
        }

        public void DisplayMessage(string message)
        {
            this.txtLog.AppendText(message + "\n");
            Application.DoEvents();
        }

        public void Log(string logMessage)
        {
            this.txtLog.AppendText(logMessage + "\n");
            Application.DoEvents();
        }

        bool IRocketLauncherView.YesOrNo(string message)
        {
            throw new NotImplementedException();
        }
    }
}
