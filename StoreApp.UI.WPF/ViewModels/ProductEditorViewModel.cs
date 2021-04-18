using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using StoreApp.UI.WPF.Extensions;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Models;
using StoreApp.UI.WPF.Models.Warehouse;

namespace StoreApp.UI.WPF.ViewModels
{
    public class ProductEditorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        MapServicesStorage services = MapServicesStorage.Instance;

        
        ProductUI _product;
        public ProductUI Product
        {
            get => _product;
            set
            {
                if (value != _product)
                {
                    _product = value;
                    OnPropertyChanged(nameof(Product));
                }
            }
        }

        public ObservableCollection<CategoryUI> Categories { get; set; } = new ObservableCollection<CategoryUI>();
        public ObservableCollection<WarehouseSectionUI> Sections { get; set; } = new ObservableCollection<WarehouseSectionUI>();
        public ObservableCollection<ProvisionerUI> Provisioners { get; set; } = new ObservableCollection<ProvisionerUI>();

        public ProductEditorViewModel(int productId)
        {
            // if editing
            if (productId != 0) 
            {
                Init(productId);
            }
        }

        private async void Init(int productId)
        {
            Product = await services.ProductsMapService.GetFullProductById(productId);
            Categories.AddRange(await services.CategoriesMapService.GetAllCategories());
            Sections.AddRange(await services.WarehouseSectionsMapService.GetAllSections());
            Provisioners.AddRange(await services.ProvisionersMapService.GetAllProvisioners());
            ;
        }

        // add 
        //IsAvailable = true
        //Rating = 0
        //SelectionLabel = string.Empty
        // ArrivalDate = DateTime.Now

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
