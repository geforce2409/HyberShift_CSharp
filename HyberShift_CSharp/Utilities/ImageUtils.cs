using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace HyberShift_CSharp.Utilities
{
    public static class ImageUtils
    {
        private static readonly ImageConverter _imageConverter = new ImageConverter();

        //Image to base64 string
        public static string CopyImageToBase64String(Image theImage)
        {
            using (var memoryStream = new MemoryStream())
            {
                theImage.Save(memoryStream, theImage.RawFormat);
                var imageBytes = memoryStream.ToArray();
                var base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        //Image to byte array
        public static byte[] CopyImageToByteArray(Image theImage)
        {
            using (var memoryStream = new MemoryStream())
            {
                theImage.Save(memoryStream, ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }

        //Base64 array to Image
        public static Image GetImageFromBase64String(string base64String)
        {
            var imageBytes = Convert.FromBase64String(base64String);
            var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            var image = Image.FromStream(ms, true);
            return image;
        }

        //Byte array to Image
        public static Bitmap GetImageFromByteArray(byte[] byteArray)
        {
            var bm = (Bitmap) _imageConverter.ConvertFrom(byteArray);

            if (bm != null && (bm.HorizontalResolution != (int) bm.HorizontalResolution ||
                               bm.VerticalResolution != (int) bm.VerticalResolution))
                bm.SetResolution((int) (bm.HorizontalResolution + 0.5f),
                    (int) (bm.VerticalResolution + 0.5f));

            return bm;
        }

        //convert the Bitmap to a ImageSource
        public static BitmapImage BitmapToImageSource(Bitmap bitmap)
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

        public static BitmapImage Base64StringToBitmapSource(string base64string)
        {
            var image = GetImageFromBase64String(base64string);
            return BitmapToImageSource((Bitmap) image);
        }
    }
}