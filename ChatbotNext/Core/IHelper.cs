using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChatbotNext.Core
{
    public interface IHelper
    {
        T CopyFrom<T>(object source, bool ignoreNull = false)
            where T : new();
        string GetBase64Image(string fileName, int maxWidth, int maxHeight);
        ImageSource GetImageFromBase64(string base64);
    }
}
