using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using AForge.Controls;
using AForge.Video;
using AForge.Video.DirectShow;
using UserControl = System.Windows.Controls.UserControl;


namespace InteractiveCollages.Views
{
    /// <summary>
    /// Interaction logic for PhotoView.xaml
    /// </summary>
    public partial class PhotoView : UserControl
    {
        private VideoSourcePlayer videoSourcePlayer;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoDevice;
        private VideoCapabilities[] videoCapabilities;
        private VideoCapabilities[] snapshotCapabilities;
        private MainWindow main { get; set; }

        private bool inPreview { get; set; }
        private bool hasTakenPic = false;
        private GreenRemover greenRemover { get; }


        private int camIndex = 1;
        public PhotoView(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
            inPreview = false;
            greenRemover = new GreenRemover();
            camIndex = main.VideoIndex;
            greenRemover.minEffect = main.minGreen;
            greenRemover.maxEffect = main.maxGreen;
        }


        private void CreateVideoSourcePlayer()
        {
            
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host =
                new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the control.
            videoSourcePlayer = new VideoSourcePlayer();
            videoSourcePlayer.Width = 400;
            videoSourcePlayer.Height = 300;

            // Assign the control as the host control's child.
            host.Child = videoSourcePlayer;

            // Add the interop host control to the Grid
            // control's collection of child controls.
            this.GridVideo.Children.Add(host);

        }

        private void StartVideoSourcePlayer()
        {
            videoDevice = new VideoCaptureDevice(videoDevices[camIndex].MonikerString);
            videoCapabilities = videoDevice.VideoCapabilities;
            snapshotCapabilities = videoDevice.SnapshotCapabilities;


            if (videoDevice != null)
            {
                if ((videoCapabilities != null) && (videoCapabilities.Length != 0))
                {
                    videoDevice.VideoResolution = videoCapabilities[camIndex];
                }

                if ((snapshotCapabilities != null) && (snapshotCapabilities.Length != 0))
                {
                    videoDevice.ProvideSnapshots = true;
                    videoDevice.SnapshotResolution = snapshotCapabilities[camIndex];
                    videoDevice.SnapshotFrame += new NewFrameEventHandler(videoDevice_SnapshotFrame);
                }

                videoSourcePlayer.VideoSource = videoDevice;
                videoSourcePlayer.Start();
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ViewController(main).GoToView(new StartView(main));
        }


        private void Button_capture_Click(object sender, RoutedEventArgs e)
        {
            if (!inPreview)
            {
                while (!hasTakenPic)
                {
                    CaptureFrame();
                }


            }
            else if (inPreview)
            {
                ResetCamera();

            }
            
        }

        private void CaptureFrame()
        {

            if ((videoDevice != null) && (videoDevice.ProvideSnapshots))
            {
                bool ready = videoSourcePlayer.InvokeRequired;
                if (ready)
                {
                    videoDevice.SimulateTrigger();
                }
                else
                {
                    videoSourcePlayer.Invoke(new Action(() => videoDevice.SimulateTrigger()));
                }
            }
        }
        private void Capture()
        {

            //Takes Photo

            //camera.Capture();

            //Freezes the camera and show preview of photo

           // Disconnect();
            //Shows taken picture
            Bitmap preview = new Bitmap(@"../../Resources/temp/temp.png");
            preview.Dispose();
            //Image_preview.Source = Photo.AsBitmapImage(preview);

            bool ready = UserControl.Dispatcher.CheckAccess();

            if (ready)
            {
                Image_preview.Source = greenRemover.RemoveGreen();
                ButtonCapture.Content = new BitmapImage(new Uri(@"../../Resources/UI/Button_opnieuw.png", UriKind.Relative));
                ImageTevreden.Visibility = Visibility.Visible;
                ButtonContinue.Visibility = Visibility.Visible;
                GridVideo.Visibility = Visibility.Hidden;

            }
            else
            {
                Image_preview.Dispatcher.Invoke(() => { Image_preview.Source = greenRemover.RemoveGreen(); });
                ButtonCapture.Dispatcher.Invoke(() => { ButtonCapture.Content = new BitmapImage(new Uri(@"../../Resources/UI/Button_opnieuw.png", UriKind.Relative)); });
                ImageTevreden.Dispatcher.Invoke(() => { ImageTevreden.Visibility = Visibility.Visible; });
                ButtonContinue.Dispatcher.Invoke(() => { ButtonContinue.Visibility = Visibility.Visible; });
                // videoSourcePlayer.Invoke(videoSourcePlayer.Margin = new Padding(100, 0, 0, 0));
                GridVideo.Dispatcher.Invoke(() => { GridVideo.Visibility = Visibility.Hidden; });


            }
            



            inPreview = true;
        }

        private void ResetCamera()
        {
            

            //Disables and hides the preview
            Image_preview.Source = null;
            inPreview = false;

            //Returns buttons and labels to their original state
            ButtonCapture.Content = new BitmapImage(new Uri(@"../../Resources/UI/Button_makePhoto.png", UriKind.Relative));
            ImageTevreden.Visibility = Visibility.Hidden;
            ButtonContinue.Visibility = Visibility.Hidden;

            
            Disconnect();
            main.DataContext = new PhotoView(main);
        }

        private void ButtonContinue_Click(object sender, RoutedEventArgs e)
        {
            Disconnect();
            this.Focusable = false;
            main.DataContext = new CollageView(main);
        }

        private void Disconnect()
        {
            if (videoSourcePlayer.VideoSource != null)
            {
                // stop video device
                videoSourcePlayer.SignalToStop();
                videoSourcePlayer.WaitForStop();
                videoSourcePlayer.VideoSource = null;

                if (videoDevice.ProvideSnapshots)
                {
                    videoDevice.SnapshotFrame -= new NewFrameEventHandler(videoDevice_SnapshotFrame);
                }

            }
        }

        private void videoDevice_SnapshotFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Console.WriteLine(eventArgs.Frame.Size);
            if (File.Exists(@"../../Resources/temp/temp.png"))
            {
                File.Delete(@"../../Resources/temp/temp.png");
            }
            Bitmap test = (Bitmap)eventArgs.Frame.Clone();
            test.Save(@"../../Resources/temp/temp.png", ImageFormat.Png);
            test.Dispose();
            hasTakenPic = true;
            Capture();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            CreateVideoSourcePlayer();
            StartVideoSourcePlayer();
        }
    }
}
