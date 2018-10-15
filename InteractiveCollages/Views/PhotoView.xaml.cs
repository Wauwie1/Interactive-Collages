using System;
using System.Collections.Generic;
using System.Drawing;
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

        private bool inPreview { get; set; }
        public PhotoView(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
            inPreview = false;
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
            if (!inPreview)
            {
                Capture();

            }
            else if (inPreview)
            {
                ResetCamera();
            }
            
        }

        private void Capture()
        {
            //Takes Photo
            camera.Capture();
            //Freezes the camera and show preview of photo
            //Shows taken picture
            Bitmap preview = new Bitmap(@"../../Resources/temp/temp.png");
            //Image_preview.Source = Photo.AsBitmapImage(preview);
            Image_preview.Source = GreenRemover.RemoveGreen();
            preview.Dispose();

            //Changes button
            ButtonCapture.Content = new BitmapImage(new Uri(@"../../Resources/UI/Button_opnieuw.png", UriKind.Relative));

            ImageTevreden.Visibility = Visibility.Visible;
            ButtonContinue.Visibility = Visibility.Visible;
            inPreview = true;
        }

        private void ResetCamera()
        {
            //Resets the camera
            camera.Reset();

            //Disables and hides the preview
            Image_preview.Source = null;
            inPreview = false;

            //Returns buttons and labels to their original state
            ButtonCapture.Content = new BitmapImage(new Uri(@"../../Resources/UI/Button_makePhoto.png", UriKind.Relative));
            ImageTevreden.Visibility = Visibility.Hidden;
            ButtonContinue.Visibility = Visibility.Hidden;
        }

        private void ButtonContinue_Click(object sender, RoutedEventArgs e)
        {
            new ViewController(main).GoToView(new StartView(main));
        }
    }
}
