using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using StoreApp.DAL;
using StoreApp.UI.WPF.Commands;
using StoreApp.UI.WPF.Extensions;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Helpers.ExtraModels;
using StoreApp.UI.WPF.Helpers.Validators;
using StoreApp.UI.WPF.Models;
using StoreApp.UI.WPF.Models.ExtraModels;
using StoreApp.UI.WPF.Models.Warehouse;

namespace StoreApp.UI.WPF.ViewModels
{
    public class ArchiveViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        MapServicesStorage services = MapServicesStorage.Instance;
        ArchiveValidator validator = new ArchiveValidator(); 

        public ObservableCollection<WroteOffProductExtraModel> WroteOffProducts { get; set; } = new ObservableCollection<WroteOffProductExtraModel>();
        public ObservableCollection<SoldProductExtraModel> SoldProducts { get; set; } = new ObservableCollection<SoldProductExtraModel>();

        public event Action NoWroteOffProductsFoundEvent;
        public event Action SomeWroteOffProductsFoundEvent;
        public event Action NoSoldProductsFoundEvent;
        public event Action SomeSoldProductsFoundEvent;

        public void Start()
        {
            InitListBoxes();
            InitDates();
        }

        private async void InitListBoxes()
        {
            WroteOffProducts.AddRange
            (
                 await services.WroteOffProductsMapService.GetLastProducts()
            );

            SoldProducts.AddRange
            (
                await services.SoldProductsMapService.GetLastProducts()
            );

            CheckProductsCount();
        }

        private void CheckProductsCount()
        {
            if (WroteOffProducts.Count == 0)
            {
                NoWroteOffProductsFoundEvent?.Invoke();
            }
            else
            {
                SomeWroteOffProductsFoundEvent?.Invoke();
            }

            if (SoldProducts.Count == 0)
            {
                NoSoldProductsFoundEvent?.Invoke();
            }
            else
            {
                SomeSoldProductsFoundEvent?.Invoke();
            }
        }


        #region Dates

        DateTime _dateFrom;
        public DateTime DateFrom
        {
            get => _dateFrom;
            set
            {
                if (value != _dateFrom)
                {
                    _dateFrom = value;
                    OnPropertyChanged(nameof(DateFrom));
                }
            }
        }

        DateTime _dateTo;
        public DateTime DateTo
        {
            get => _dateTo;
            set
            {
                if (value != _dateTo)
                {
                    _dateTo = value;
                    OnPropertyChanged(nameof(DateTo));
                }
            }
        }

        private void InitDates()
        {
            DateFrom = DateTime.Now.AddDays(-1);
            DateTo = DateTime.Now;
        }

        #endregion

        #region Show Range
        private ProcessCommand _showRangeCommand;

        public ProcessCommand ShowRangeCommand => _showRangeCommand ?? (_showRangeCommand = new ProcessCommand(obj =>
        {
            ShowRange();
        }));

        private async void ShowRange()
        {
            if(validator.IsDatesValid(DateFrom, DateTo))
            {
                WroteOffProducts.Clear();
                SoldProducts.Clear();

                WroteOffProducts.AddRange
                (
                    await services.WroteOffProductsMapService.GetProductsByRange(DateFrom, DateTo)
                );
                SoldProducts.AddRange
                (
                    await services.SoldProductsMapService.GetProductsByRange(DateFrom, DateTo)
                );

                CheckProductsCount();
            }
        }
        #endregion

        #region ShowOrdersArchiveCommand
        private ProcessCommand _showOrdersArchiveCommand;

        public ProcessCommand ShowOrdersArchiveCommand => _showOrdersArchiveCommand ?? (_showOrdersArchiveCommand = new ProcessCommand(obj =>
        {
            if (Directory.Exists(Settings.OrdersDirectoryArchiveFolder))
            {
                Process.Start("explorer.exe", Settings.OrdersDirectoryArchiveFolder);
            }
            else
            {
                MessageBox.Show("Archive directory with orders has been deleted", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }));
        #endregion


        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
