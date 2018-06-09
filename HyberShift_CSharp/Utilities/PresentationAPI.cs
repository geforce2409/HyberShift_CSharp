using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;

namespace HyberShift_CSharp.Utilities
{
    public class PresentationAPI
    {
        public ObservableCollection<string> LoadAndExportBase64(string path, int width, int height)
        {
            var paths = ExportSlide(path, width, height);
            var bitmaps = ImportSlide(paths);
            var base64Arr = ConvertToBase64(bitmaps);

            return base64Arr;
        }

        public ObservableCollection<string> LoadAndExportBase64(string path)
        {
            var paths = ExportSlide(path);
            var bitmaps = ImportSlide(paths);
            var base64Arr = ConvertToBase64(bitmaps);

            return base64Arr;
        }

        public ObservableCollection<string> ExportSlide(string path, int width, int height)
        {
            var paths = new ObservableCollection<string>();

            var pptApplication = new ApplicationClass();
            var pptPresentation = pptApplication.Presentations.Open(path, MsoTriState.msoFalse,
                MsoTriState.msoFalse, MsoTriState.msoFalse);

            var counter = 0;
            foreach (Slide slide in pptPresentation.Slides)
            {
                var destpath = Environment.CurrentDirectory + "\\slide" + counter + ".png";
                slide.Export(destpath, "png", width, height);
                paths.Add(destpath);
                counter++;
            }

            return paths;
        }

        public ObservableCollection<string> ExportSlide(string path)
        {
            var paths = new ObservableCollection<string>();

            var pptApplication = new ApplicationClass();
            var pptPresentation = pptApplication.Presentations.Open(path, MsoTriState.msoFalse,
                MsoTriState.msoFalse, MsoTriState.msoFalse);

            var counter = 0;
            foreach (Slide slide in pptPresentation.Slides)
            {
                var destpath = Environment.CurrentDirectory + "\\slide" + counter + ".png";
                slide.Export(destpath, "png");
                paths.Add(destpath);
                counter++;
            }

            return paths;
        }

        public ObservableCollection<Bitmap> ImportSlide(ObservableCollection<string> paths)
        {
            var bitmaps = new ObservableCollection<Bitmap>();
            foreach (var path in paths)
            {
                var tempBitmap = new Bitmap(path);
                bitmaps.Add(tempBitmap);
            }

            return bitmaps;
        }

        public ObservableCollection<string> ConvertToBase64(ObservableCollection<Bitmap> bitmaps)
        {
            var base64Array = new ObservableCollection<string>();
            foreach (var bitmap in bitmaps)
            {
                // convert to byte
                var stream = new MemoryStream();
                bitmap.Save(stream, ImageFormat.Bmp);
                var bytes = stream.ToArray();

                // convert to base64
                var base64String = Convert.ToBase64String(bytes);
                base64Array.Add(base64String);
            }

            return base64Array;
        }

        //public BitmapSource ConvertToBitmapSource(string base64Str)
        //{
        //    // Convert Base64 String to byte[]
        //    byte[] imageBytes = Convert.FromBase64String(base64Str);
        //    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

        //    // Convert byte[] to Image
        //    ms.Write(imageBytes, 0, imageBytes.Length);
        //    Bitmap bitmap = new Bitmap(ms, true);

        //}
    }
}