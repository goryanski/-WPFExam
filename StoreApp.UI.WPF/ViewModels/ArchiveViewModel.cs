using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
                // WroteOffProducts
                WroteOffProducts.Clear();
                WroteOffProducts.AddRange
                (
                    await services.WroteOffProductsMapService.GetProductsByRange(DateFrom, DateTo)
                );
                
                if(WroteOffProducts.Count == 0)
                {
                    
                }



                // SoldProducts
                SoldProducts.Clear();
                SoldProducts.AddRange
                (
                    await services.SoldProductsMapService.GetProductsByRange(DateFrom, DateTo)
                );

                if (SoldProducts.Count == 0)
                {

                }
            }
        }
        #endregion


        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
