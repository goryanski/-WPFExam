using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using StoreApp.UI.WPF.Models.Warehouse;

namespace StoreApp.UI.WPF.Helpers.Validators
{
    class ProductValidator
    {
        ProductUI product = new ProductUI();

        internal bool IsProductValid(ProductUI product)
        {
            this.product = product;

            try
            {
                CheckComboboxes();
                CheckName();
                CheckWeight();
                CheckPrice();
                CheckImage();
                CheckDate();
                CheckAmount();
                return true;
            }
            catch (InvalidDataException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

        private void CheckAmount()
        {
            if (product.AmountInStorage <= 0)
            {
                throw new InvalidDataException("Incorrect entered amount in storage.");
            }
        }

        private void CheckDate()
        {
            if (product.SellBy < DateTime.Now)
            {
                throw new InvalidDataException("Incorrect entered maximum date of product sale (field SellBy)" +
                    "\nDate must be later than now");
            }
        }

        private void CheckImage()
        {
            if(product.PhotoPath is null || product.PhotoPath == string.Empty)
            {
                product.PhotoPath = Settings.DefaultImagePath;
            }
        }

        private void CheckPrice()
        {
            if (product.PrimeCost <= 0)
            {
                throw new InvalidDataException("Incorrect entered Prime Cost.");
            }
            if (product.Price <= 0)
            {
                throw new InvalidDataException("Incorrect entered Price.");
            }
        }

        private void CheckWeight()
        {
            if (product.Weight <= 0 || product.Weight > 1000)
            {
                throw new InvalidDataException("Incorrect entered weight.");
            }
        }

        private void CheckName()
        {
            if (!Regex.IsMatch(product.Name, "^[a-zA-Z][a-zA-Z0-9 ]{2,29}$"))
            {
                throw new InvalidDataException("Incorrect entered product name." +
                    "\nmust be 3-30 symbols, English language, first a letter, only letters, gigits, spaces");
            }
        }

        private void CheckComboboxes()
        {
            if (product.Category is null)
            {
                throw new InvalidDataException("Category is not selected.");
            }
            else if(product.Provisioner is null)
            {
                throw new InvalidDataException("Provisioner is not selected.");
            }
            else if (product.Section is null)
            {
                throw new InvalidDataException("Section is not selected.");
            }
        }
    }
}
