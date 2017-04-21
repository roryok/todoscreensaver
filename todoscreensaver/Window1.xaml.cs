using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Web;
using System.Net;
using System.IO;
using System.Windows.Documents;
using System.Text.RegularExpressions;
using System.Windows.Threading;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace todoscreensaver
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        /// <summary>
        /// Settings for the app, from App.Settings
        /// </summary>
        public Settings Settings = new Settings();

        /// <summary>
        /// Variable to hold exceptions
        /// </summary>
        public Exception LastException { get; set; }

        private bool isFirstTime;

        public Window1()
        {
            InitializeComponent();

            // load settings from registry
            Settings.Load();

            isFirstTime = false;

            try
            {
                // warn user if datapath is not set
                if (string.IsNullOrEmpty(Settings.DataPath))
                {
                    Filename.Text = "Please choose a file in screensaver settings!";
                }
                else
                {
                    // load content from file
                    string content = System.IO.File.ReadAllText(Settings.DataPath);
                    Content.Text = content;
                    Filename.Text = Settings.DataPath;
                }
            }
            catch(Exception err)
            {
                // catch errors loading data 
                // store exception in state so we can show the full data later if prompted
                LastException = err; 
                Filename.Text = "Error loading " + Settings.DataPath;
                Content.Text = err.Message;
            }
        }                

        /// <summary>
        /// close app on mousemove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isFirstTime) {
                isFirstTime = true;
                return;
            }
            Application.Current.Shutdown();
        }

        /// <summary>
        /// handle keydown events: show exception detail on F4, close app on all other keys
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F4)
            {
                // show last exception if present
                if (LastException != null)
                {
                    Content.Text = LastException.ToString().Replace(" at", Environment.NewLine + " at");
                }
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}

