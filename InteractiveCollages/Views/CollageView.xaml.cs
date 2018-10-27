using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;
using PixelFormat = System.Windows.Media.PixelFormat;
using UserControl = System.Windows.Controls.UserControl;

namespace InteractiveCollages.Views
{
    /// <summary>
    /// Interaction logic for CollageView.xaml
    /// </summary>
    public partial class CollageView : UserControl
    {
        
        private MainWindow main;
        public CollageView(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
            main.ViewControl.Height = 900;
            RandomCollage();
        }

        private void ButtonShare_OnClick(object sender, RoutedEventArgs e)
        {
            //Create a new bitmap.
            var bmpScreenshot = new Bitmap(550,
                550);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);


                //647,250
                //390, 262
            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(364,
                203,
                0,
                0,
                bmpScreenshot.Size,
                CopyPixelOperation.SourceCopy);

            Guid filename = Guid.NewGuid();
            
            // Save the screenshot to the specified path that the user has chosen.
            bmpScreenshot.Save(@"C:\Users\Paul_Laptop\Dropbox\Expo fotos\" +  filename + ".png", ImageFormat.Png);
            main.DataContext = new ShareView(main);
        }

        private void RandomCollage()
        {
            //Refresh user photo
            try
            {
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

                var bitmapImage = new BitmapImage();
                var stream = File.OpenRead(path + "\\Resources\\temp\\temp.png");

                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                stream.Close();
                stream.Dispose();





                ImageUser.Source = bitmapImage;
              
            }
            catch (NullReferenceException e)
            {
                const string error = "Could not load user photo. \n";
                Console.WriteLine(error + e.Source);
                MessageBox.Show(error + e.Source);
            }

            //Pick random collage
            string randomPath = CollageMaker.GetRandomCollage();
            Bitmap bitmap = new Bitmap(randomPath);
            ImageCollage.Source = Photo.AsBitmapImage(bitmap);
            bitmap.Dispose();
        }

        private void ButtonOver_Click(object sender, RoutedEventArgs e)
        {
            RandomCollage();
        }

        private void ButtonRestart_Click(object sender, RoutedEventArgs e)
        {
            new ViewController(main).GoToView(new StartView(main));
        }
    }
}
