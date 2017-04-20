using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Win32;

namespace todoscreensaver
{
    public class Settings
    {
        /// <summary>
        /// the path to your desired text file
        /// </summary>
        public string DataPath { get; set; }
        
        /// <summary>
        /// persist settings to registry
        /// </summary>
        public void Save()
        {
            // Create or get existing Registry subkey
            RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\todoscreensaver");
            key.SetValue("file", DataPath);
        }

        /// <summary>
        /// load settings from registry
        /// </summary>
        public void Load()
        {
            // Get the value stored in the Registry
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\todoscreensaver");
            if (key != null)
                DataPath = (string)key.GetValue("file");
        }
    }
}
