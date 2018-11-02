using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InteractiveCollages.Views
{
    /// <summary>
    ///     Interaction logic for CollageView.xaml
    /// </summary>
    public partial class CollageView : UserControl
    {
        private readonly MainWindow main;

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
            gfxScreenshot.CopyFromScreen(397,
                236,
                0,
                0,
                bmpScreenshot.Size,
                CopyPixelOperation.SourceCopy);

            var filename = Guid.NewGuid();

            // Save the screenshot to the specified path that the user has chosen.
            bmpScreenshot.Save(@"C:\Users\Paul_Laptop\Dropbox\Expo fotos\" + filename + ".png", ImageFormat.Png);
            main.DataContext = new ShareView(main);
        }

        private void RandomCollage()
        {
            //Refresh user photo
            try
            {
                var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

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
            var randomPath = main.CollageMaker.GetRandomCollage();
            var bitmap = new Bitmap(randomPath);
            ImageCollage.Source = Photo.AsBitmapImage(bitmap);
            bitmap.Dispose();
        }

        private void ButtonOver_Click(object sender, RoutedEventArgs e)
        {
            RandomCollage();
        }

        private void ButtonRestart_Click(object sender, RoutedEventArgs e)
        {
            main.DataContext = new StartView(main);
        }
    }
}