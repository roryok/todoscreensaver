using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace todoscreensaver
{
    public partial class SettingsWindow : Window
    {
        private Settings settings;
 
        public SettingsWindow()
        {
            InitializeComponent();
 
            // Load settings from file (or just set to default values
            // if file not found)
            settings = new Settings();
            settings.Load();
            // bxWindowsLiveId.Text = settings.WindowsLiveId;       

            switch (settings.ScreensaverCloseMode) {
                case CloseMode.MOUSE_MOVE:
                    radioButton_DeacMouseMove.IsChecked = true;
                    break;
                case CloseMode.MOUSE_DOWN:
                default:
                    radioButton_DeacMouseDown.IsChecked = true;
                    break;
            }
        }
 
        /// <summary>
        /// close settings window, triggering save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            settings.Save();
            this.Close();
        }

        /// <summary>
        /// choose txt file to display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fld = new OpenFileDialog();
            DialogResult result = fld.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                bxPath.Text = fld.FileName;
                settings.DataPath = fld.FileName;
                settings.Save();
            }
        }

        private void radioButton_DeacMouseMove_Checked(object sender, RoutedEventArgs e) {
            settings.ScreensaverCloseMode = CloseMode.MOUSE_MOVE;
        }

        private void radioButton_DeacMouseDown_Checked(object sender, RoutedEventArgs e) {
            settings.ScreensaverCloseMode = CloseMode.MOUSE_DOWN;
        }
    }
}
