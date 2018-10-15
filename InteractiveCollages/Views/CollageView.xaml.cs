﻿using System;
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
            string userPath = "../../Resources/temp/temp.png";
            Bitmap userBitmap = new Bitmap(userPath);
            ImageUser.Source = Photo.AsBitmapImage(userBitmap);

            //Pick random collage
            string randomPath = CollageMaker.GetRandomCollage();
            Bitmap bitmap = new Bitmap(randomPath);
            ImageCollage.Source = Photo.AsBitmapImage(bitmap);
        }
    }
}
