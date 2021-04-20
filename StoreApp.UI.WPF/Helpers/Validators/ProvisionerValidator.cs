using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using StoreApp.UI.WPF.Models;

namespace StoreApp.UI.WPF.Helpers.Validators
{
    public class ProvisionerValidator
    {
        ProvisionerUI provisioner = new ProvisionerUI();

        internal bool IsProvisionerValid(ProvisionerUI provisioner)
        {
            this.provisioner = provisioner;

            try
            {
                CheckName();
                CheckEmail();
                return true;
            }
            catch (InvalidDataException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

        private void CheckName()
        {
            if (!Regex.IsMatch(provisioner.Name, "^[a-zA-Z][a-zA-Z0-9 ]{2,29}$"))
            {
                throw new InvalidDataException("Incorrect entered provisioner name." +
                    "\nmust be 3-30 symbols, English language, first a letter, only letters, gigits, spaces");
            }
        }
        private void CheckEmail()
        {
            string emailChecking = @"^[a-zA-Z0-9][a-zA-Z0-9!#$%&'*+-\/=?^_`{|}~]{2,63}@[a-zA-Z]{2,32}\.[a-zA-Z]{2,16}$";

            if (!Regex.IsMatch(provisioner.Mail, emailChecking))
            {
                throw new InvalidDataException("Incorrect entered provisioner mail.");
            }
        }
    }
}
