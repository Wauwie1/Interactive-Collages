using System;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InteractiveCollages
{
    public class GreenRemover
    {
        public int maxEffect = 42;
        public int minEffect = 8;

        public WriteableBitmap RemoveGreen()
        {
            //var uri = new Uri(@"../../Resources/temp/temp.png");
            var bitmap = new Bitmap(@"../../Resources/temp/temp.png");
            var photo = Photo.AsBitmapImage(bitmap);
            bitmap.Dispose();
            var input = new WriteableBitmap(photo);


            var outputA = new WriteableBitmap(Convert.ToInt32(input.PixelWidth), Convert.ToInt32(input.PixelHeight), 96,
                96, PixelFormats.Bgra32, null);

            // Iterate over all pixels from top to bottom...
            for (var y = 0; y < Convert.ToInt32(input.PixelHeight); y++)
                // ...and from left to right
            for (var x = 0; x < Convert.ToInt32(input.PixelWidth); x++)
            {
                // Determine the pixel color
                var camColor = input.GetPixel(x, y);

                // Every component (red, green, and blue) can have a value from 0 to 255, so determine the extremes
                var max = Math.Max(Math.Max(camColor.R, camColor.G), camColor.B);
                var min = Math.Min(Math.Min(camColor.R, camColor.G), camColor.B);

                // Should the pixel be masked/replaced?
                var replace =
                    camColor.G != min // green is not the smallest value
                    && (camColor.G == max // green is the biggest value
                        || max - camColor.G < minEffect) // or at least almost the biggest value
                    && max - min > maxEffect; // minimum difference between smallest/biggest value (avoid grays)

                if (!replace) outputA.SetPixel(x, y, camColor);
            }

            SaveTemp(@"../../Resources/temp/temp.png", outputA.Clone());

            return outputA;
        }

        private static void SaveTemp(string filename, BitmapSource image)
        {
            if (File.Exists(@"../../Resources/temp/temp.png"))
                try
                {
                    File.Delete(@"../../Resources/temp/temp.png");
                }
                catch (IOException e)
                {
                    Console.WriteLine(e);
                }

            if (filename != string.Empty)
                using (var stream = new FileStream(filename, FileMode.Create))
                {
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image));
                    encoder.Save(stream);
                }
        }
    }
}