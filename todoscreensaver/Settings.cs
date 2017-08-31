using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media;
using System.ComponentModel;

namespace todoscreensaver
{
    public enum CloseMode {
        NONE = 0,
        MOUSE_DOWN = 1,
        MOUSE_MOVE = 2
    }

    public class Settings: INotifyPropertyChanged
    {
        #region INotifiedProperty Block
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        /// <summary>
        /// the path to your desired text file
        /// </summary>
        public string _dataPath;
        public string DataPath
        {
            get
            {
                return _dataPath;
            }

            set
            {
                _dataPath = value;
                OnPropertyChanged("DataPath");
            }
        }

        /// <summary>
        /// background color
        /// </summary>
        private SolidColorBrush _background;
        public SolidColorBrush BackgroundColor
        {
            get
            {
                return _background;
            }

            set
            {
                _background = value;
                OnPropertyChanged("BackgroundColor");
            }
        }

        /// <summary>
        /// color of text 
        /// </summary>
        private SolidColorBrush _foreground;
        public SolidColorBrush ForegroundColor
        {
            get
            {
                return _foreground;
            }

            set
            {
                _foreground = value;
                OnPropertyChanged("ForegroundColor");
            }
        }

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
            key.SetValue("background_color", BackgroundColor.ToString());
            key.SetValue("foreground_color", ForegroundColor.ToString());
        }

        /// <summary>
        /// load settings from registry
        /// </summary>
        public void Load()
        {
            // Get the value stored in the Registry
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\todoscreensaver");
            if (key != null) {
                DataPath = (string)key.GetValue("file", "");
                ScreensaverCloseMode = (CloseMode)key.GetValue("close_mode", CloseMode.MOUSE_DOWN);
                BackgroundColor = (SolidColorBrush)(new BrushConverter().ConvertFrom((string)key.GetValue("background_color", "#FFFFFF")));
                ForegroundColor = (SolidColorBrush)(new BrushConverter().ConvertFrom((string)key.GetValue("foreground_color", "#000000")));
            }
        }
    }
}
