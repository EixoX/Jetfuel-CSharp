using EixoX.Interceptors;
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
        private Dictionary<string, string> _defaultSettings;


        /// <summary>
        /// Default dictionary of settings from a default settings file
        /// </summary>
        public Dictionary<string, string> DefaultSettings
        {
            get
            {
                if (_defaultSettings == null)
                {
                    Dictionary<string, string> KeyValueSettings = new Dictionary<string, string>();
                    string[] settings = System.IO.File.ReadAllText(DefaultSettingsFile).Split('\n');

                    foreach (string setting in settings)
                    {
                        string[] kvp = setting.Split('>');
                        if (kvp.Length == 2)
                            if (!KeyValueSettings.ContainsKey(kvp[0]))
                            {
                                string key = Whitespace.Collapse(kvp[0]).Replace("\r", "");
                                string value = Whitespace.Collapse(kvp[1]).Replace("\r", "");
                                KeyValueSettings.Add(key, value);
                            }
                    }

                    _defaultSettings = KeyValueSettings;
                }

                return _defaultSettings;
            }
        }

        public abstract string Directory { get; }

        public string DefaultSettingsFile { get { return this.Directory + "\\settings.eixox"; } }
    }
}
