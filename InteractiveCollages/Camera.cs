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
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Threading;


using Microsoft.Win32;
using WebEye.Controls.Wpf;
using System.Reflection;

namespace InteractiveCollages
{
    class Camera
    {
        //Fields

        private bool inPreview = false;
        private BitmapImage takenPhoto;
        IEnumerable<WebCameraId> idList;

        private WebCameraControl webCameraControl;

        //Constructor
        public Camera(WebCameraControl webcam)
        {
            webCameraControl = webcam;

            //Sets timer
           
            
        }

        //Custom methods
        public void ActivateCamera()
        {
            //Gets a list of all available webcams and selects the first one
            idList = webCameraControl.GetVideoCaptureDevices();
            var id = idList.ElementAt(0);
            //Activates the webcam
            webCameraControl.StartCapture(id);
        }
        public void Capture()
        {
            //Takes picture and converts it to a WriteableBitmap
            Bitmap photoBitmap = webCameraControl.GetCurrentImage();

            

            if (File.Exists(@"../../Resources/temp/temp.png"))
            {
                try
                {
                    File.SetAttributes(@"../../Resources/temp/temp.png", FileAttributes.Normal);
                    File.Delete(@"../../Resources/temp/temp.png");
                    File.SetAttributes(@"../../Resources/temp/temp.png", FileAttributes.Normal);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e);
                }
                

            }

            photoBitmap.Save(@"../../Resources/temp/temp.png", System.Drawing.Imaging.ImageFormat.Png);
            photoBitmap.Dispose();





            //Stops and hides camera
            webCameraControl.StopCapture();
            webCameraControl.Visibility = Visibility.Hidden;

        }

        public void Reset()
        {
            ActivateCamera();
            webCameraControl.Visibility = Visibility.Visible;
        }


    }
}
