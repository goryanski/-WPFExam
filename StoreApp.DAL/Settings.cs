using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.DAL
{
    public class Settings
    {
        public static string DefaultImagePath { get; set; } = "DefaultImage\\no-image.png";
        public static string OrdersDirectoryFolder { get; set; } = "OrdersDirectory";
        public static string OrdersDirectoryArchiveFolder { get; set; } = "OrdersDirectoryArchive";
        public static string DownloadImagesFolder { get; set; } = "DownloadImages";
    }
}
