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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InteractiveCollages.Views
{
    /// <summary>
    /// Interaction logic for PhotoView.xaml
    /// </summary>
    public partial class PhotoView : UserControl
    {
        private MainWindow main { get; set; }
        private Camera camera { get; set; }
        public PhotoView(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
            camera = new Camera(webCameraControl);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ViewController(main).GoToView(new StartView(main));
        }

        private void webCameraControl_Loaded(object sender, RoutedEventArgs e)
        {
            camera.ActivateCamera();
        }

        private void Button_capture_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
