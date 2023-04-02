using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace ChatbotNext.Core
{

    public class Helper : IHelper
    {

        public T CopyFrom<T>(object source, bool ignoreNull = false)
            where T : new()
        {
            var json = JsonSerializer.Serialize(source);
            var target = JsonSerializer.Deserialize<T>(json);
            return target;
        }

        public string GetBase64Image(string fileName, int maxWidth, int maxHeight)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(fileName);
            bitmapImage.EndInit();

            double width = bitmapImage.PixelWidth;
            double height = bitmapImage.PixelHeight;

            if (width > maxWidth)
            {
                height = (height / width) * maxWidth;
                width = maxWidth;
            }

            if (height > maxHeight)
            {
                width = (width / height) * maxHeight;
                height = maxHeight;
            }

            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            encoder.QualityLevel = 100;

            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                byte[] imageBytes = stream.ToArray();

                BitmapImage compressedBitmap = new BitmapImage();
                compressedBitmap.BeginInit();
                compressedBitmap.StreamSource = new MemoryStream(imageBytes);
                compressedBitmap.DecodePixelWidth = (int)width;
                compressedBitmap.DecodePixelHeight = (int)height;
                compressedBitmap.EndInit();

                byte[] compressedBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    var encoder2 = new PngBitmapEncoder();
                    encoder2.Frames.Add(BitmapFrame.Create(compressedBitmap));
                    encoder2.Save(ms);
                    compressedBytes = ms.ToArray();
                }

                return Convert.ToBase64String(compressedBytes);
            }
        }

        public ImageSource GetImageFromBase64(string base64)
        {
            var bytes = Convert.FromBase64String(base64);
            using var ms = new MemoryStream(bytes);
            ms.Position = 0;
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = ms;
            bitmapImage.EndInit();
            return bitmapImage;
        }
    }
}
