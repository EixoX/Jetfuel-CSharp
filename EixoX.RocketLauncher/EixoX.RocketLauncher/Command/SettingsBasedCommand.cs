using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher
{
    /// <summary>
    /// Class based on settings created by user
    /// </summary>
    public abstract class SettingsBasedCommand
    {
        /// <summary>
        /// Default dictionary of settings from a default settings file
        /// </summary>
        public Dictionary<string, string> DefaultSettings
        {
            get
            {
                Dictionary<string, string> KeyValueSettings = new Dictionary<string, string>();
                string[] settings = System.IO.File.ReadAllText(DefaultSettingsFilePath).Split(';');

                foreach (string setting in settings)
                {
                    string[] kvp = setting.Split('=');
                    if (kvp.Length == 2)
                        if (!KeyValueSettings.ContainsKey(kvp[0]))
                            KeyValueSettings.Add(kvp[0], kvp[1]);
                }

                return KeyValueSettings;
            }
        }

        public string DefaultSettingsFilePath { get { return AppDomain.CurrentDomain.BaseDirectory + "\\settings.eixox"; } }
    }
}
