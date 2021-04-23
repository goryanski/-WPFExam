using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace StoreApp.UI.WPF.Helpers.Validators
{
    public class ArchiveValidator
    {
        internal bool IsDatesValid(DateTime dateFrom, DateTime dateTo)
        {
            try
            {

                CheckDateFrom(dateFrom);
                CheckDateTo(dateTo);
                return true;
            }
            catch (InvalidDataException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

        private void CheckDateFrom(DateTime dateFrom)
        {
            if (dateFrom < DateTime.Now.AddYears(-30))
            {
                throw new InvalidDataException("Incorrect entered date from" +
                    "\nIt's too long ago");
            }
        }

        private void CheckDateTo(DateTime dateTo)
        {
            if (dateTo > DateTime.Now)
            {
                throw new InvalidDataException("Incorrect entered date to" +
                   "\nDate must be earlier than now ");
            }
            
        }
    }
}
