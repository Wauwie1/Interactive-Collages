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
using AForge.Controls;
using AForge.Video;
using AForge.Video.DirectShow;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace InteractiveCollages
{
    /// <summary>
    /// Interaction logic for CameraSettings.xaml
    /// </summary>
    public partial class CameraSettings : Window
    {
        private MainWindow main;
        public CameraSettings(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void Button_SetVidIndex_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                main.VideoIndex = Convert.ToInt32(TextBox_VidIndex.Text);
                MessageBox.Show("VideoIndex set.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_SetGreen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                main.minGreen = Convert.ToInt32(TextBox_Min.Text);
                main.maxGreen = Convert.ToInt32(TextBox_Max.Text);
                MessageBox.Show("Greenscreen properties set.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
