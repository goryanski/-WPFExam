using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.DAL
{
    public class Settings
    {
        // Images
        public static string DefaultImagePath { get; set; } = "DefaultImage\\no-image.png";
        public static string DownloadImagesFolder { get; set; } = "DownloadImages";


        // Orders Directories
        public static string OrdersDirectoryFolder { get; set; } = "OrdersDirectory";
        public static string OrdersDirectoryArchiveFolder { get; set; } = "OrdersDirectoryArchive";


        // SendOrdersService
        public static string MailFrom { get; } = "mytestpost200@gmail.com";
        public static string MailPassword { get; } = "12345123ii";
        public static string SenderName { get; } = "Products warehouse 'Polyana'";
    }
}
