using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace InteractiveCollages
{
    public static class Photo
    {
        //BitmapImage conversions
        public static BitmapImage AsBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;
                var bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        //WriteableBitmap conversions
        public static WriteableBitmap AsWriteableBitmap(Bitmap bitmap)
        {
            var bitmapImage = AsBitmapImage(bitmap);
            var writeableBitmap = AsWriteableBitmap(bitmapImage);

            return writeableBitmap;
        }

        public static WriteableBitmap AsWriteableBitmap(BitmapImage bitmapImage)
        {
            return new WriteableBitmap(bitmapImage);
        }
    }
}