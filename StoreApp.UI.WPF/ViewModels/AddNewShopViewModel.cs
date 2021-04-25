using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using StoreApp.UI.WPF.Commands;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Helpers.Validators;
using StoreApp.UI.WPF.Models;

namespace StoreApp.UI.WPF.ViewModels
{
    public class AddNewShopViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event Action ShopWasAddedEvent;
        MapServicesStorage services = MapServicesStorage.Instance;
        ShopValidator validator = new ShopValidator();

        string _shopName;
        public string ShopName
        {
            get => _shopName;
            set
            {
                if (value != _shopName)
                {
                    _shopName = value;
                    OnPropertyChanged(nameof(ShopName));
                }
            }
        }


        private ProcessCommand _addShopCommand;
        public ProcessCommand AddShopCommand => _addShopCommand ?? (_addShopCommand = new ProcessCommand(obj =>
        {
            AddShop();
        }));
        private async void AddShop()
        {
            if (validator.IsShopNameValid(ShopName))
            {
                ShopUI shop = new ShopUI { Name = ShopName };
                await services.ShopsMapService.CreateShop(shop);
                ShopWasAddedEvent?.Invoke();
            }
        }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
