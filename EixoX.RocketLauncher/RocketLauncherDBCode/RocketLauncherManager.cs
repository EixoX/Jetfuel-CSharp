using EixoX.Restrictions;
using EixoX.RocketLauncher;
using EixoX.RocketLauncher.DatabaseGathering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RocketLauncherDBCode
{
    public class RocketLauncherManager
    {
        private List<Label> ConnectionStringObservers;
        private List<Label> DirectoryObservers;
        private string _ConnStr;
        private string _Dir;

        public RocketLauncherViewee Viewee { get; set; }

        [Required]
        public string ConnectionString {
            get { return this._ConnStr; }
            set
            {
                this._ConnStr = value;
                foreach (Label lbl in ConnectionStringObservers)
                    lbl.Text = value;
            }
        }

        [Required]
        public string Directory
        {
            get { return this._Dir; }
            set
            {
                this._Dir = value;
                foreach (Label lbl in DirectoryObservers)
                    lbl.Text = value;
            }
        }

        public void WatchConnectionString(Label label)
        {
            this.ConnectionStringObservers.Add(label);
        }

        public void WatchDirectory(Label label)
        {
            this.DirectoryObservers.Add(label);
        }

        public RocketLauncherManager()
        {
            this.ConnectionStringObservers = new List<Label>();
            this.DirectoryObservers = new List<Label>();
        }

        private static RocketLauncherManager _Instance;

        public static RocketLauncherManager Instance
        {
            get
            {
                if (_Instance == null) 
                    _Instance = new RocketLauncherManager();

                return _Instance;
            }
        }

        public bool IsValid()
        {
            return RestrictionAspect<RocketLauncherManager>.Instance.Validate(this);
        }

        public bool Connect(DataGridView grid)
        {
            try
            {
                List<GenericDatabaseTable> tables = new DatabaseTableLister().Execute(this.Viewee, this.ConnectionString).ToList();
                grid.DataSource = tables;
                return true;
            }
            catch (Exception ex)
            {
                this.Viewee.OnException(ex);
                return false;
            }
        }

    }
}
