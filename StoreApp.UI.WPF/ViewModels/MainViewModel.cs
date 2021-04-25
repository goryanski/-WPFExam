using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using StoreApp.BLL.DTO;
using StoreApp.BLL.DTO.ExtraTables;
using StoreApp.BLL.DTO.Warehouse;
using StoreApp.DAL.Repositories;
using StoreApp.UI.WPF.Commands;
using StoreApp.UI.WPF.Extensions;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Helpers.Validators;
using StoreApp.UI.WPF.Models;
using StoreApp.UI.WPF.Models.ExtraModels;
using StoreApp.UI.WPF.Models.Warehouse;
using StoreApp.UI.WPF.Services.Warehouse;

namespace StoreApp.UI.WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        MapServicesStorage services = MapServicesStorage.Instance;
        public ObservableCollection<ShopUI> Shops { get; set; } = new ObservableCollection<ShopUI>();
        public ObservableCollection<ProductUI> Products { get; set; } = new ObservableCollection<ProductUI>();
        public ObservableCollection<CategoryUI> Categories { get; set; } = new ObservableCollection<CategoryUI>();


        public event Action ProductsLoadCompleteEvent;
        public event Action NoProductsFoundEvent;
        public event Action SomeProductsFoundEvent;


        public MainViewModel()
        {
            DataInit();
        }

        private async void DataInit()
        {
            Categories.AddRange(await services.CategoriesMapService.GetAllCategories());
            Shops.AddRange(await services.ShopsMapService.GetAllShops());

            await LoadToListBoxDefaultProducts();
            CheckProductsCount();
            ProductsLoadCompleteEvent?.Invoke();
        }

        private async Task LoadToListBoxDefaultProducts()
        {
            if (Categories[0] != null)
            {
                Products.AddRange(await services.ProductsMapService.GetProductsByCategory(Categories[0].Id));
            }
        }


        #region GetProductsBySelectedCategory
        CategoryUI _selectedCategory;
        public CategoryUI SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (value != _selectedCategory)
                {
                    _selectedCategory = value;
                    OnPropertyChanged(nameof(SelectedCategory));
                    GetProductsBySelectedCategory();
                }
            }
        }

        private async void GetProductsBySelectedCategory()
        {
            Products.Clear();
            Products.AddRange(await services.ProductsMapService.GetProductsByCategory(SelectedCategory.Id));
            CheckProductsCount();
        }
        #endregion

        #region TextBoxSearchProducts
        // catch here TextBoxSearch text change
        string _str = "";
        public string TextBoxSearchText
        {
            get => _str;
            set
            {
                if (value != _str)
                {
                    _str = value;
                    FindProductsBySearchText(_str);
                }
            }
        }
        private async void FindProductsBySearchText(string srchText)
        {
            Products.Clear();
            Products.AddRange(await services.ProductsMapService.GetProductsBySearchText(srchText));
            CheckProductsCount();
        }
        #endregion

        #region Top panel

        #region Sorting 

        #region ShowNewProductsCommand
        private ProcessCommand _showNewProductsCommand;

        public ProcessCommand ShowNewProductsCommand => _showNewProductsCommand ?? (_showNewProductsCommand = new ProcessCommand(obj =>
        {
            GetNewProducts();
        }));

        private async void GetNewProducts()
        {
            Products.Clear();
            Products.AddRange(await services.ProductsMapService.GetNewProducts());
            CheckProductsCount();
        }
        #endregion

        #region ShowMostPopularProductsCommand
        private ProcessCommand _showMostPopularProductsCommand;

        public ProcessCommand ShowMostPopularProductsCommand => _showMostPopularProductsCommand ?? (_showMostPopularProductsCommand = new ProcessCommand(obj =>
        {
            GetMostPopularProducts();
        }));

        private async void GetMostPopularProducts()
        {
            Products.Clear();
            Products.AddRange(await services.ProductsMapService.GetMostPopularProducts());
            CheckProductsCount();
        }
        #endregion

        #region ShowProductsToOrderCommand
        private ProcessCommand _showProductsToOrderCommand;

        public ProcessCommand ShowProductsToOrderCommand => _showProductsToOrderCommand ?? (_showProductsToOrderCommand = new ProcessCommand(obj =>
        {
            GetProductsToOrder();
        }));

        private async void GetProductsToOrder()
        {
            Products.Clear();
            Products.AddRange(await services.ProductsMapService.GetProductsToOrder());
            CheckProductsCount();
        }
        #endregion

        #region ShowProductsWriteOffSoonCommand
        private ProcessCommand _showProductsWriteOffSoonCommand;

        public ProcessCommand ShowProductsWriteOffSoonCommand => _showProductsWriteOffSoonCommand ?? (_showProductsWriteOffSoonCommand = new ProcessCommand(obj =>
        {
            GetProductsWriteOffSoon();
        }));

        private async void GetProductsWriteOffSoon()
        {
            Products.Clear();
            Products.AddRange(await services.ProductsMapService.GetProductsWriteOffSoon());
            CheckProductsCount();
        }
        #endregion

        #region OverdueCommand
        private ProcessCommand _overdueCommand;

        public ProcessCommand OverdueCommand => _overdueCommand ?? (_overdueCommand = new ProcessCommand(obj =>
        {
            GetProductsOverdue();
        }));

        private async void GetProductsOverdue()
        {
            Products.Clear();
            Products.AddRange(await services.ProductsMapService.GetProductsOverdue());
            CheckProductsCount();
        }
        #endregion

        #endregion


        #endregion

        #region Right panel

        RightPanelValidator validator = new RightPanelValidator();
        public ProductUI ListBoxSelectedProduct { get; set; }

        enum RIghtPanelActions
        {
            Delele,
            WriteOff,
            SendToShop,
            PutToBasket,
            Edit
        }

        #region Edit product

        public event Action<int> StartEditProductEvent;

        private ProcessCommand _editProductCommand;

        public ProcessCommand EditProductCommand => _editProductCommand ?? (_editProductCommand = new ProcessCommand(obj =>
        {
            RunAction(RIghtPanelActions.Edit);
        }));


        // for add and edit product 
        internal async void UpdateProducts()
        {
            // we don't know what is new product category and what is current filter was selected for products list (new products, popular products...), so just refresh products list - we load products by default category, and rest will be update automatically
            Products.Clear();
            if (SelectedCategory != Categories[0])
            {
                SelectedCategory = Categories[0];
            }
            else
            {
                // if we was at the same category, when adding or editing of product - reload this category products list
                await LoadToListBoxDefaultProducts();
            }
        }
        #endregion

        #region  Delele, WriteOff, SendToShop, PutToBasket

        // products count has been entered for actions in right panel
        public string TextBoxCountProducts { get; set; }
        

        #region Delete product
        public event Action InputFieldsCanBeClearedEvent;

        private ProcessCommand _deleteProductByCountCommand;

        public ProcessCommand DeleteProductByCountCommand => _deleteProductByCountCommand ?? (_deleteProductByCountCommand = new ProcessCommand(obj =>
        {
            RunAction(RIghtPanelActions.Delele);
        }));

        private async void DeleteProductByCount(int newAmountInStorageValue, bool selectAll, ProductUI selectedProduct)
        {
            await DeleteProductFromDbAndUI(newAmountInStorageValue, selectAll, selectedProduct);
        }
        #endregion

        #region Write Off product
        private ProcessCommand _writeOffCommand;

        public ProcessCommand WriteOffCommand => _writeOffCommand ?? (_writeOffCommand = new ProcessCommand(obj =>
        {
            RunAction(RIghtPanelActions.WriteOff);
        }));
        private async void WriteOffProduct(int newAmountInStorageValue, bool selectAll, ProductUI selectedProduct, int countToSend)
        {
            await WriteOff(selectedProduct, countToSend);
            await DeleteProductFromDbAndUI(newAmountInStorageValue, selectAll, selectedProduct);
        }

        private async Task WriteOff(ProductUI selectedProduct, int countToSend)
        {
            WroteOffProductUI product = new WroteOffProductUI
            {
                ProductId = selectedProduct.Id,
                Amount = countToSend,
                Date = DateTime.Now
            };

            await services.WroteOffProductsMapService.CreateProduct(product);
        }

        internal async void UpdateShopsList()
        {
            Shops.Add(await services.ShopsMapService.GetLastAddedShop());
        }

        #endregion

        #region Put product To Basket
        private ProcessCommand _toBasketCommand;

        public ProcessCommand ToBasketCommand => _toBasketCommand ?? (_toBasketCommand = new ProcessCommand(obj =>
        {
            RunAction(RIghtPanelActions.PutToBasket);
        }));

        private async void PutProductToBasket(int newAmountInStorageValue, bool selectAll, ProductUI selectedProduct, int countToSend)
        {
            await SendProductToBasket(selectedProduct, countToSend);
            //await DeleteProductFromDbAndUI(newAmountInStorageValue, selectAll, selectedProduct);
            InputFieldsCanBeClearedEvent?.Invoke();
        }

        private async Task SendProductToBasket(ProductUI selectedProduct, int countToSend)
        {
            OrderUI order = new OrderUI
            {
                ProductName = selectedProduct.Name,
                ProvisionerId = selectedProduct.ProvisionerId,
                CountToOrder = countToSend,
                OrderedProdictId = selectedProduct.Id
            };

            await services.OrdersMapService.CreateOrder(order);
        }
        #endregion

        #region Add product To Shop 

        ShopUI _selectedShop;
        public ShopUI SelectedShop
        {
            get => _selectedShop;
            set
            {
                if (value != _selectedShop)
                {
                    _selectedShop = value;
                    OnPropertyChanged(nameof(SelectedShop));
                }
            }
        }

        private ProcessCommand _addToShopCommand;
        public ProcessCommand SendToShopCommand => _addToShopCommand ?? (_addToShopCommand = new ProcessCommand(obj =>
        {
            if(SelectedShop is null)
            {
                MessageBox.Show("Shop to send isn't selected", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            RunAction(RIghtPanelActions.SendToShop);
        }));

        private async void SendProductToShop(int newAmountInStorageValue, bool selectAll, ProductUI selectedProduct, int countToSend)
        {
            await SendToShop(selectedProduct, countToSend);
            await DeleteProductFromDbAndUI(newAmountInStorageValue, selectAll, selectedProduct);

            if (await services.SoldProductsMapService.UpdateProductRating(selectedProduct))
            {
                UpdateProducts();
            }
        }

        private async Task SendToShop(ProductUI selectedProduct, int countToSend)
        {
            SoldProductUI product = new SoldProductUI
            {
                ProductId = selectedProduct.Id,
                Amount = countToSend,
                ShopId = SelectedShop.Id,
                SoldDate = DateTime.Now
            };

            await services.SoldProductsMapService.CreateProduct(product);
        }
        #endregion

        private void RunAction(RIghtPanelActions action)
        {
            // if user didn't selected product (first time, when user didn't selected product by launch app )
            if (ListBoxSelectedProduct is null)
            {
                MessageBox.Show("Select product first", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // if user selected a product, then changed a category or did something and the product disappeared from the listbox - he must select the product again
            var selectedProduct = Products.FirstOrDefault(p => p.Id == ListBoxSelectedProduct.Id);
            if (selectedProduct is null)
            {
                MessageBox.Show("Product not selected", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if(action == RIghtPanelActions.Edit)
            {
                StartEditProductEvent?.Invoke(selectedProduct.Id);
                return;
            }


            int countToDelete;
            int.TryParse(TextBoxCountProducts, out countToDelete);

            int countInStock = selectedProduct.AmountInStorage;

            if (validator.IsProductsCountValid(countToDelete, countInStock))
            {
                bool selectAll = false;
                if (countToDelete == countInStock)
                {
                    selectAll = true;
                }

                int newAmountInStorageValue = countInStock - countToDelete;

                switch (action)
                {
                    case RIghtPanelActions.Delele:
                        DeleteProductByCount(newAmountInStorageValue, selectAll, selectedProduct);
                        break;
                    case RIghtPanelActions.WriteOff:
                        WriteOffProduct(newAmountInStorageValue, selectAll, selectedProduct, countToDelete);
                        break;
                    case RIghtPanelActions.SendToShop:
                        SendProductToShop(newAmountInStorageValue, selectAll, selectedProduct, countToDelete);
                        break;
                    case RIghtPanelActions.PutToBasket:
                        PutProductToBasket(newAmountInStorageValue, selectAll, selectedProduct, countToDelete);
                        break;
                }
            }
        }


        private async Task DeleteProductFromDbAndUI(int newAmountInStorageValue, bool selectAll, ProductUI selectedProduct)
        {
            // change product.AmountInStorage in DB
            await services.ProductsMapService.DeleteProductByCount(ListBoxSelectedProduct, newAmountInStorageValue);
            if (selectAll)
            {
                // set IsAvailable = false
                await services.ProductsMapService.DeleteWholeProduct(ListBoxSelectedProduct);
                // Remove from ObservableCollection
                Products.Remove(selectedProduct);
            }
            else
            {
                // change field AmountInStorage in ObservableCollection
                selectedProduct.AmountInStorage = newAmountInStorageValue;
            }

            InputFieldsCanBeClearedEvent?.Invoke();
        }


        #endregion

        #region Add category 
        public string NewCategoryName { get; set; }

        private ProcessCommand _addCategoryCommand;

        public ProcessCommand AddCategoryCommand => _addCategoryCommand ?? (_addCategoryCommand = new ProcessCommand(obj =>
        {
            if (validator.IsCategoryNameValid(NewCategoryName))
            {
                CategoryUI newCategory = new CategoryUI { Name = NewCategoryName };
                services.CategoriesMapService.CreateCategory(newCategory);
                Categories.Add(newCategory);
            }
        }));
        #endregion

        #endregion

        private void CheckProductsCount()
        {
            if (Products.Count == 0)
            {
                NoProductsFoundEvent?.Invoke();
            }
            else
            {
                SomeProductsFoundEvent?.Invoke();
            }
        }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
