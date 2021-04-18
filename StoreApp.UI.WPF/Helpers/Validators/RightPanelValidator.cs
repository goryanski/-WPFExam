using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace StoreApp.UI.WPF.Helpers.Validators
{
    public class RightPanelValidator
    {
        // CategoryName
        internal bool IsCategoryNameValid(string categoryName)
        {
            try
            {
                CheckCategoryName(categoryName);
                return true;
            }
            catch (InvalidDataException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

        private void CheckCategoryName(string categoryName)
        {
            if (!Regex.IsMatch(categoryName, "^[a-zA-Z][a-zA-Z0-9_]{3,12}$"))
            {
                throw new InvalidDataException("Incorrect entered category name." +
                    "\nmust be 4-12 symbols, first a letter, only letters, digits, and symbol _");
            }
        }



        // ProductsCount
        internal bool IsProductsCountValid(int productsCount, int amountProductsInStock)
        {
            try
            {
                CheckProductsCount(productsCount, amountProductsInStock);
                return true;
            }
            catch (InvalidDataException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }
        private void CheckProductsCount(int productsCount, int amountProductsInStock)
        {
            if(productsCount > amountProductsInStock)
            {
                throw new InvalidDataException("There're not enough products in stock");
            }
            //else if(count == amountProductsInStock)
            //{
            //    throw new InvalidDataException("Use button 'Delete all' to delete all products");
            //}
            else if(productsCount <= 0)
            {
                throw new InvalidDataException("Incorrect entered products count.");
            }
        }
    }
}
