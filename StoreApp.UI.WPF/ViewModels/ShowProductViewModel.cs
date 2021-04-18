using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using StoreApp.UI.WPF.Commands;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Models.Warehouse;

namespace StoreApp.UI.WPF.ViewModels
{
    public class ShowProductViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        MapServicesStorage services = MapServicesStorage.Instance;

        ProductUI _selectedProduct;
        public ProductUI SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                if (value != _selectedProduct)
                {
                    _selectedProduct = value;
                    OnPropertyChanged(nameof(SelectedProduct));
                }
            }
        }

        public ShowProductViewModel(int productId)
        {
            GetFullProduct(productId);
        }

        private async void GetFullProduct(int id)
        {
            SelectedProduct = await services.ProductsMapService.GetFullProductById(id);
        }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
