﻿using System;
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
 
            // Load settings from file (or just set to default values if file not found)
            settings = new Settings();
            settings.Load();

            bxPath.Text = settings.DataPath;     

            switch (settings.ScreensaverCloseMode) {
                case CloseMode.MOUSE_MOVE:
                    radioButton_DeacMouseMove.IsChecked = true;
                    break;
                case CloseMode.MOUSE_DOWN:
                default:
                    radioButton_DeacMouseDown.IsChecked = true;
                    break;
            }

            ClrPcker_Background.SelectedColor = settings.BackgroundColor.Color;
            ClrPcker_Foreground.SelectedColor = settings.ForegroundColor.Color;
                        
        }
 
        /// <summary>
        /// close settings window, triggering save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            settings.DataPath = bxPath.Text;
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
            }
        }


        private void radioButton_DeacMouseMove_Checked(object sender, RoutedEventArgs e) {
            settings.ScreensaverCloseMode = CloseMode.MOUSE_MOVE;
        }

        private void radioButton_DeacMouseDown_Checked(object sender, RoutedEventArgs e) {
            settings.ScreensaverCloseMode = CloseMode.MOUSE_DOWN;
        }

        /// <summary>
        /// handle background color picker change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            var x = ((Xceed.Wpf.Toolkit.ColorPicker)sender).SelectedColor;
            settings.BackgroundColor = new SolidColorBrush((Color)x);
        }

        /// <summary>
        /// handle foreground color picker change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClrPcker_Foreground_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            var x = ((Xceed.Wpf.Toolkit.ColorPicker)sender).SelectedColor;
            settings.ForegroundColor = new SolidColorBrush((Color)x);
        }
    }
}
