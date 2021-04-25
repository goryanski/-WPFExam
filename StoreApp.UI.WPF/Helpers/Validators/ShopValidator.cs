using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace StoreApp.UI.WPF.Helpers.Validators
{
    public class ShopValidator
    {
        internal bool IsShopNameValid(string shopName)
        {
            try
            {
                CheckShopName(shopName);
                return true;
            }
            catch (InvalidDataException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

        private void CheckShopName(string shopName)
        {
            if (shopName is null)
            {
                throw new InvalidDataException("Shop name cannot be empty");
            }
            if (!Regex.IsMatch(shopName, "^[a-zA-Z][a-zA-Z0-9 ]{2,29}$"))
            {
                throw new InvalidDataException("Incorrect entered shop name." +
                    "\nmust be 3-30 symbols, English language, first a letter, only letters, gigits, spaces");
            }
        }
    }
}
