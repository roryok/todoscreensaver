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
    public enum CloseMode {
        NONE = 0,
        MOUSE_DOWN = 1,
        MOUSE_MOVE = 2
    }

    public class Settings
    {
        /// <summary>
        /// the path to your desired text file
        /// </summary>
        public string DataPath { get; set; }

        /// <summary>
        /// the mode indicating how the screensaver will close
        /// (on mouse click (mouse down) or mouse move)
        /// </summary>
        public CloseMode ScreensaverCloseMode { get; set; }
        
        /// <summary>
        /// persist settings to registry
        /// </summary>
        public void Save()
        {
            // Create or get existing Registry subkey
            RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\todoscreensaver");
            key.SetValue("file", DataPath);
            key.SetValue("close_mode", (int)ScreensaverCloseMode);
        }

        /// <summary>
        /// load settings from registry
        /// </summary>
        public void Load()
        {
            // Get the value stored in the Registry
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\todoscreensaver");
            if (key != null) {
                DataPath = (string)key.GetValue("file");
                ScreensaverCloseMode = (CloseMode)key.GetValue("close_mode", CloseMode.MOUSE_DOWN);
            }
        }
    }
}
