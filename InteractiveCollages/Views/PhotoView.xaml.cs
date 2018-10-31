using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using AForge.Controls;
using AForge.Video;
using AForge.Video.DirectShow;

namespace InteractiveCollages.Views
{
    /// <summary>
    ///     Interaction logic for PhotoView.xaml
    /// </summary>
    public partial class PhotoView : UserControl
    {
        private readonly int camIndex = 1;
        private int countdown = 3;
        private bool hasTakenPic;
        private VideoCapabilities[] snapshotCapabilities;
        private VideoCapabilities[] videoCapabilities;
        private VideoCaptureDevice videoDevice;
        private FilterInfoCollection videoDevices;
        private VideoSourcePlayer videoSourcePlayer;

        public PhotoView(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
            inPreview = false;
            greenRemover = new GreenRemover();
            camIndex = main.VideoIndex;
            greenRemover.minEffect = main.minGreen;
            greenRemover.maxEffect = main.maxGreen;
            CreateTimer();
        }

        private MainWindow main { get; }

        private bool inPreview { get; set; }
        private GreenRemover greenRemover { get; }
        private DispatcherTimer dispatcherTimer { get; set; }

        private void CreateTimer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (countdown == 3)
            {
                ImageCountdown3.Visibility = Visibility.Visible;
                countdown--;
            }
            else if (countdown == 2)
            {
                ImageCountdown2.Visibility = Visibility.Visible;
                ImageCountdown3.Visibility = Visibility.Hidden;
                countdown--;
            }
            else if (countdown == 1)
            {
                ImageCountdown2.Visibility = Visibility.Hidden;
                ImageCountdown1.Visibility = Visibility.Visible;
                countdown--;
            }
            else
            {
                ImageCountdown1.Visibility = Visibility.Hidden;

                dispatcherTimer.Stop();

                while (!hasTakenPic) CaptureFrame();
            }
        }

        private void CreateVideoSourcePlayer()
        {
            // Create the interop host control.
            var host =
                new WindowsFormsHost();

            // Create the control.
            videoSourcePlayer = new VideoSourcePlayer();
            videoSourcePlayer.Width = 400;
            videoSourcePlayer.Height = 300;

            // Assign the control as the host control's child.
            host.Child = videoSourcePlayer;

            // Add the interop host control to the Grid
            // control's collection of child controls.
            GridVideo.Children.Add(host);
        }

        private void StartVideoSourcePlayer()
        {
            videoDevice = new VideoCaptureDevice(videoDevices[camIndex].MonikerString);
            videoCapabilities = videoDevice.VideoCapabilities;
            snapshotCapabilities = videoDevice.SnapshotCapabilities;


            if (videoDevice != null)
            {
                if (videoCapabilities != null && videoCapabilities.Length != 0)
                    videoDevice.VideoResolution = videoCapabilities[camIndex];

                if (snapshotCapabilities != null && snapshotCapabilities.Length != 0)
                {
                    videoDevice.ProvideSnapshots = true;
                    videoDevice.SnapshotResolution = snapshotCapabilities[camIndex];
                    videoDevice.SnapshotFrame += videoDevice_SnapshotFrame;
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
            greenRemover.minEffect = main.minGreen;
            greenRemover.maxEffect = main.maxGreen;

            if (!inPreview)
            {
                ButtonCapture.Visibility = Visibility.Hidden;
                ImageCountdown3.Visibility = Visibility.Visible;
                dispatcherTimer.Start();
            }
            else if (inPreview)
            {
                ResetCamera();
            }
        }

        private void CaptureFrame()
        {
            if (videoDevice != null && videoDevice.ProvideSnapshots)
            {
                var ready = videoSourcePlayer.InvokeRequired;
                if (ready)
                    videoDevice.SimulateTrigger();
                else
                    videoSourcePlayer.Invoke(new Action(() => videoDevice.SimulateTrigger()));
            }
        }

        private void Capture()
        {
            //Takes Photo

            //camera.Capture();

            //Freezes the camera and show preview of photo

            // Disconnect();
            //Shows taken picture
            var preview = new Bitmap(@"../../Resources/temp/temp.png");
            preview.Dispose();
            //Image_preview.Source = Photo.AsBitmapImage(preview);

            var ready = UserControl.Dispatcher.CheckAccess();

            if (ready)
            {
                Image_preview.Source = greenRemover.RemoveGreen();
                ButtonCapture.Content =
                    new BitmapImage(new Uri(@"../../Resources/UI/Button_opnieuw.png", UriKind.Relative));
                ImageTevreden.Visibility = Visibility.Visible;
                ButtonContinue.Visibility = Visibility.Visible;
                ButtonCapture.Visibility = Visibility.Visible;
                GridVideo.Visibility = Visibility.Hidden;
            }
            else
            {
                Image_preview.Dispatcher.Invoke(() => { Image_preview.Source = greenRemover.RemoveGreen(); });
                ButtonCapture.Dispatcher.Invoke(() =>
                {
                    ButtonCapture.Content =
                        new BitmapImage(new Uri(@"../../Resources/UI/Button_opnieuw.png", UriKind.Relative));
                    ButtonCapture.Visibility = Visibility.Visible;
                });
                ImageTevreden.Dispatcher.Invoke(() => { ImageTevreden.Visibility = Visibility.Visible; });

                ButtonContinue.Dispatcher.Invoke(() => { ButtonContinue.Visibility = Visibility.Visible; });
                GridVideo.Dispatcher.Invoke(() => { GridVideo.Visibility = Visibility.Hidden; });
            }


            inPreview = true;
        }

        private void ResetCamera()
        {
            Disconnect();
            main.DataContext = new PhotoView(main);
        }

        private void ButtonContinue_Click(object sender, RoutedEventArgs e)
        {
            Disconnect();
            Focusable = false;
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

                if (videoDevice.ProvideSnapshots) videoDevice.SnapshotFrame -= videoDevice_SnapshotFrame;
            }
        }

        private void videoDevice_SnapshotFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Console.WriteLine(eventArgs.Frame.Size);
            if (File.Exists(@"../../Resources/temp/temp.png")) File.Delete(@"../../Resources/temp/temp.png");
            var test = (Bitmap) eventArgs.Frame.Clone();
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