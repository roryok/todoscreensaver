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
using System.Windows.Media;
using System.ComponentModel;
using System.Reflection;
using Markdig;

namespace todoscreensaver
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window, INotifyPropertyChanged
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
        /// Settings for the app, from App.Settings
        /// </summary>
        // public Settings Settings = new Settings();

        private Settings _settings;
        public Settings Settings
        {
            get
            {
                return _settings;
            }

            set
            {
                _settings = value;
                OnPropertyChanged("Settings");
            }
        }
        
        /// <summary>
        /// Variable to hold exceptions
        /// </summary>
        public Exception LastException { get; set; }

        private bool isFirstTime;

        public Window1()
        {
            InitializeComponent();
            DataContext = this;

            // load settings from registry
            Settings = new Settings();
            Settings.Load();

            switch (Settings.ScreensaverCloseMode) {
                case CloseMode.MOUSE_DOWN:
                    this.MouseMove -= Window_MouseMove;
                    this.MouseDown += Window_MouseDown;
                    break;
                case CloseMode.MOUSE_MOVE:
                    this.MouseMove += Window_MouseMove;
                    this.MouseDown -= Window_MouseDown;
                    break;
                default:
                    break;
            }

            isFirstTime = false;

            try
            {
                // warn user if datapath is not set
                if (string.IsNullOrEmpty(Settings.DataPath))
                {
                    Filename.Text = "Please choose a file in screensaver settings!";
                    Browser.NavigateToString("Please choose a file in screensaver settings!");
                }
                else
                {
                    // load content from file
                    string content = System.IO.File.ReadAllText(Settings.DataPath, Encoding.UTF8);

                    Console.WriteLine(content);

                    // convert markdown to html
                    var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
                    content = Markdown.ToHtml(content, pipeline);

                    Console.WriteLine(content);   // prints: <p>This is a text with some <em>emphasis</em></p>
                    
                    string html = @"<html>
<head>
    <meta charset='UTF-8'>
    <style>
        html, body { 
            font-family: Calibri;
            background: #" + Settings.BackgroundColor.Color.ToString().Substring(3) + @";
            color: #" + Settings.ForegroundColor.Color.ToString().Substring(3) + @";
        }        
    </style>
</head>
<body>    
    <div id='content'>" + content + @"
    </div>
</body>
</html>";

                    Browser.NavigateToString(html);
                    Filename.Text = Settings.DataPath;
                }
            }
            catch(Exception err)
            {
                // catch errors loading data 
                // store exception in state so we can show the full data later if prompted
                LastException = err; 
                Filename.Text = "Error loading " + Settings.DataPath;
                // Content.Text = err.Message;
                Browser.NavigateToString(err.Message);
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
        /// close app on mousedown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
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
                    // Content.Text = LastException.ToString().Replace(" at", Environment.NewLine + " at");
                    Browser.NavigateToString(LastException.ToString().Replace(" at", Environment.NewLine + " at"));
                }
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}

