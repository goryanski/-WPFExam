using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using StoreApp.UI.WPF.Commands;
using StoreApp.UI.WPF.Extensions;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Helpers.Validators;
using StoreApp.UI.WPF.Models;
using StoreApp.UI.WPF.Models.Warehouse;

namespace StoreApp.UI.WPF.ViewModels
{
    public class ProductEditorViewModel : INotifyPropertyChanged
    {
        enum Act
        {
            Add,
            Edit
        }

        Act action;

        public event PropertyChangedEventHandler PropertyChanged;
        MapServicesStorage services = MapServicesStorage.Instance;

        int _id;
        public int SelectedProductId
        {
            get => _id;
            set
            {
                if (value != _id)
                {
                    _id = value;
                    Init();
                }
            }
        }

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

        #region Some product fields
        CategoryUI _category;
        public CategoryUI SelectedCategory
        {
            get => _category;
            set
            {
                if (value != _category)
                {
                    _category = value;
                    OnPropertyChanged(nameof(SelectedCategory));
                }
            }
        }

        WarehouseSectionUI _section;
        public WarehouseSectionUI SelectedSection
        {
            get => _section;
            set
            {
                if (value != _section)
                {
                    _section = value;
                    OnPropertyChanged(nameof(SelectedSection));
                }
            }
        }

        ProvisionerUI _provisioner;
        public ProvisionerUI SelectedProvisioner
        {
            get => _provisioner;
            set
            {
                if (value != _provisioner)
                {
                    _provisioner = value;
                    OnPropertyChanged(nameof(SelectedProvisioner));
                }
            }
        }

        DateTime _date;
        public DateTime ProductMaximumSaleDate
        {
            get => _date;
            set
            {
                if (value != _date)
                {
                    _date = value;
                    OnPropertyChanged(nameof(ProductMaximumSaleDate));
                }
            }
        }
        #endregion

        public ObservableCollection<CategoryUI> Categories { get; set; } = new ObservableCollection<CategoryUI>();
        public ObservableCollection<WarehouseSectionUI> Sections { get; set; } = new ObservableCollection<WarehouseSectionUI>();
        public ObservableCollection<ProvisionerUI> Provisioners { get; set; } = new ObservableCollection<ProvisionerUI>();


        private async void Init()
        {
            // if editing show product info and set state
            if (SelectedProductId > 0)
            {
                action = Act.Edit;
                Product = await services.ProductsMapService.GetFullProductById(SelectedProductId);
            }
            else
            {
                action = Act.Add;
                Product = new ProductUI();
                // for displaying when adding
                ProductMaximumSaleDate = DateTime.Now.AddDays(1);
            }

            Categories.AddRange(await services.CategoriesMapService.GetAllCategories());
            Sections.AddRange(await services.WarehouseSectionsMapService.GetAllSections());
            Provisioners.AddRange(await services.ProvisionersMapService.GetAllProvisioners());
        }

        #region Add Provisioner 
        public string ProvisionerName { get; set; }
        public string ProvisionerMail { get; set; }
        ProvisionerValidator provisionerValidator = new ProvisionerValidator();
        public event Action ProvisionerWasCreatedEvent;

        private ProcessCommand _addProvisionerCommand;

        public ProcessCommand AddProvisionerCommand => _addProvisionerCommand ?? (_addProvisionerCommand = new ProcessCommand(obj =>
        {
            ProvisionerUI provisioner = new ProvisionerUI
            {
                Name = ProvisionerName,
                Mail = ProvisionerMail
            };

            if (provisionerValidator.IsProvisionerValid(provisioner))
            {
                CreateProvisioner(provisioner);
            }
        }));

        private async void CreateProvisioner(ProvisionerUI provisioner)
        {
            await services.ProvisionersMapService.CreateProvisioner(provisioner);
            Provisioners.Add(provisioner);
            ProvisionerWasCreatedEvent?.Invoke();
        }
        #endregion

        #region Save product

        ProductValidator productValidator = new ProductValidator();
        public event Action OperationCompleteEvent;
        private ProcessCommand _saveCommand;

        public ProcessCommand SaveCommand => _saveCommand ?? (_saveCommand = new ProcessCommand(obj =>
        {
            SaveProduct();
        }));

        private async void SaveProduct()
        {
            // thanks to two-way binding, we can use the same object for validation, instead of use text boxes in the UI to get the entered information 
            // except comboboxes and datePicker
            GetWindowFieldsInfo();
            if (productValidator.IsProductValid(Product))
            {
                if(action == Act.Add)
                {
                    // add additional fields that the user should not fill out
                    Product.IsAvailable = true;
                    Product.Rating = 0;
                    Product.SelectionLabel = string.Empty;
                    Product.ArrivalDate = DateTime.Now;

                    await services.ProductsMapService.CreateProduct(Product);
                    OperationCompleteEvent?.Invoke();
                }
                else if(action == Act.Edit)
                {
                    await services.ProductsMapService.UpdateProduct(Product);
                    OperationCompleteEvent?.Invoke();
                }
            }
        }

        private void GetWindowFieldsInfo()
        {          
            if(SelectedCategory != null)
            {
                Product.Category = SelectedCategory;
                Product.CategoryId = SelectedCategory.Id;
            }
            
            if(SelectedProvisioner != null)
            {
                Product.Provisioner = SelectedProvisioner;
                Product.ProvisionerId = SelectedProvisioner.Id;
            }
           
            if (SelectedSection != null)
            {
                Product.Section = SelectedSection;
                Product.SectionId = SelectedSection.Id;
            }

            Product.SellBy = ProductMaximumSaleDate;
        }
        #endregion

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
