using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using StoreApp.BLL.DTO;
using StoreApp.UI.WPF.Models;

namespace StoreApp.UI.WPF.Helpers.Validators
{
    public class UserValidator
    {
        UserUI user = new UserUI();

        internal bool IsUserValid(UserUI user)
        {
            this.user = user;

            try
            {
                CheckLogin();
                CheckPassword();
                return true;
            }
            catch (InvalidDataException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

        private void CheckLogin()
        {
            if(user.Login is null)
            {
                throw new InvalidDataException("Login cannot be empty");
            }
            else if (!Regex.IsMatch(user.Login, "^[a-zA-Z][a-zA-Z0-9_]{3,12}$"))
            {
                throw new InvalidDataException("Incorrect entered login." +
                    "\nmust be 4-12 symbols, first a letter, only letters, digits, and symbol _");
            }
        }
        private void CheckPassword()
        {
            if (user.Password is null)
            {
                throw new InvalidDataException("Password cannot be empty");
            }
            else if (!Regex.IsMatch(user.Password, "^[a-zA-Z0-9_]{4,12}$"))
            {
                throw new InvalidDataException("Incorrect entered password." +
                    "\nmust be 4-12 symbols, only letters, digits, and symbol _");
            }
        }
    }
}
