using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace InteractiveCollages
{
    public static class Photo
    {
        //Bitmap conversions
        public static Bitmap AsBitmap(BitmapImage bitmapImage)
        {
            throw new NotImplementedException();
        }

        public static Bitmap AsBitmap(WriteableBitmap writeableBitmap)
        {
            throw new NotImplementedException();
        }


        //BitmapImage conversions
        public static BitmapImage AsBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        public static BitmapImage AsBitmapImage(WriteableBitmap writeableBitmap)
        {
            throw new NotImplementedException();
        }

        //WriteableBitmap conversions
        public static WriteableBitmap AsWriteableBitmap(Bitmap bitmap)
        {
            BitmapImage bitmapImage = AsBitmapImage(bitmap);
            WriteableBitmap writeableBitmap = AsWriteableBitmap(bitmapImage);

            return writeableBitmap;
        }
        public static WriteableBitmap AsWriteableBitmap(BitmapImage bitmapImage)
        {
            return new WriteableBitmap(bitmapImage);
        }
    }
}
