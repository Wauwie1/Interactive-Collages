using System;
using System.Windows;

namespace InteractiveCollages
{
    /// <summary>
    ///     Interaction logic for CameraSettings.xaml
    /// </summary>
    public partial class CameraSettings : Window
    {
        private readonly MainWindow main;

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
                main.GreenRemover.minEffect = Convert.ToInt32(TextBox_Min.Text);
                main.GreenRemover.maxEffect = Convert.ToInt32(TextBox_Max.Text);
                MessageBox.Show("Greenscreen properties set.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}