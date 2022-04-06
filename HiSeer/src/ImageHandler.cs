using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace HiSeer.src
{
    public static class ImageHandler
    {
        public static Image CreateImage(string path)
        {
            Image icon = new Image();
            Uri uri = new Uri(path);
            icon.Source = new BitmapImage(uri);
            icon.Width = 280;

            return icon;
        }
    }
}
