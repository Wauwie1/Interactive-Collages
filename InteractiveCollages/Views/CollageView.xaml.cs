using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            throw new NotImplementedException();
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
                const string error = "Could not find user photo. \n";
                Console.WriteLine(error, e.Source);
                MessageBox.Show("Could not find user photo. \n", e.Source);
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
