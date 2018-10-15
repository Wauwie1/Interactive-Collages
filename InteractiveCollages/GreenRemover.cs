using System;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Windows.Media.Color;

namespace InteractiveCollages
{
    public static class GreenRemover
    {
        private static int minEffect = 8;
        private static int maxEffect = 42;

        public static WriteableBitmap RemoveGreen()
        {
            //var uri = new Uri(@"../../Resources/temp/temp.png");
            Bitmap bitmap = new Bitmap(@"../../Resources/temp/temp.png");
            BitmapImage photo = Photo.AsBitmapImage(bitmap);
            bitmap.Dispose();
            WriteableBitmap input = new WriteableBitmap(photo);




            WriteableBitmap outputA = new WriteableBitmap(Convert.ToInt32(input.PixelWidth), Convert.ToInt32(input.PixelHeight), 96, 96, PixelFormats.Bgra32, null);

            // Iterate over all pixels from top to bottom...
            for (int y = 0; y < Convert.ToInt32(input.PixelHeight); y++)
            {
                // ...and from left to right
                for (int x = 0; x < Convert.ToInt32(input.PixelWidth); x++)
                {
                    // Determine the pixel color
                    Color camColor = input.GetPixel(x, y);

                    // Every component (red, green, and blue) can have a value from 0 to 255, so determine the extremes
                    byte max = Math.Max(Math.Max(camColor.R, camColor.G), camColor.B);
                    byte min = Math.Min(Math.Min(camColor.R, camColor.G), camColor.B);

                    // Should the pixel be masked/replaced?
                    bool replace =
                        camColor.G != min // green is not the smallest value
                        && (camColor.G == max // green is the biggest value
                            || max - camColor.G < minEffect) // or at least almost the biggest value
                        && (max - min) > maxEffect; // minimum difference between smallest/biggest value (avoid grays)

                    if (!replace)
                    {
                        outputA.SetPixel(x, y, camColor);
                    }

                }
            }
            SaveTemp(@"../../Resources/temp/temp.png", outputA.Clone());

            return outputA;
        }

        static void SaveTemp(string filename, BitmapSource image)
        {
            
            if (File.Exists(@"../../Resources/temp/temp.png"))
            {
                try
                {
                    File.Delete(@"../../Resources/temp/temp.png");
                }
                catch (IOException e)
                {
                    Console.WriteLine(e);
                }


            }
             if (filename != string.Empty)
            {
                using (FileStream stream = new FileStream(filename, FileMode.Create))
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image));
                    encoder.Save(stream);
                }
            }
        }
    }
}