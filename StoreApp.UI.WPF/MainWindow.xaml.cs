using StoreApp.DAL.Interfaces;
using StoreApp.DAL.Repositories;
using StoreApp.UI.WPF.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using StoreApp.UI.WPF.ViewModels;
using StoreApp.UI.WPF.Windows;
using StoreApp.UI.WPF.Models.Warehouse;

namespace StoreApp.UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /*
            User user1 = new User
            {
                Login = "admin",
                Password = "admin",
                IsAdmin = true
            };
            User user2 = new User
            {
                Login = "igor",
                Password = "1111",
                IsAdmin = false
            };
     */
    public partial class MainWindow : Window
    {
        MainViewModel viewModel = new MainViewModel();
        List<Expander> rightPanelExpanders = new List<Expander>();

        string defaultSearchText = "find product by name...";
        ProductUI previousSelectedProduct = new ProductUI();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;

            MainViewModelSubscriptions();

            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void MainViewModelSubscriptions()
        {
            viewModel.NoProductsFoundEvent += ViewModel_NoProductsFoundEvent;
            viewModel.SomeProductsFoundEvent += ViewModel_SomeProductsFoundEvent;
            viewModel.ProductsLoadCompleteEvent += ViewModel_ProductsLoadCompleteEvent;
            viewModel.DeleteProductByCountCompleteEvent += ViewModel_DeleteProductByCountCompleteEvent;
            viewModel.StartEditProductEvent += ViewModel_StartEditProductEvent;
        }

        

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // forbid to close window
            //e.Cancel = true;
        }

        #region MainViewModel Events
        //private void ViewModel_StartAddProductEvent(ProductUI product)
        //{
            
        //}

        private void ViewModel_DeleteProductByCountCompleteEvent()
        {
            tbDeleteProductCount.Text = string.Empty;
            tbWriteOff.Text = string.Empty;
            tbAddToShop.Text = string.Empty;
            tbAddToBasket.Text = string.Empty;
        }

        private void ViewModel_ProductsLoadCompleteEvent()
        {
            lblLoading.Visibility = Visibility.Hidden;
            lblPrompt.Visibility = Visibility.Visible;
        }

        private void ViewModel_SomeProductsFoundEvent()
        {
            tbNotFound.Visibility = Visibility.Hidden;
        }

        private void ViewModel_NoProductsFoundEvent()
        {
            tbNotFound.Visibility = Visibility.Visible;
        }

        #endregion


        #region Load
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            btnLogin.Content = "Login";
            tbNotFound.Visibility = Visibility.Hidden;
            lblLoading.Visibility = Visibility.Visible;
            lblPrompt.Visibility = Visibility.Hidden;
            SetDefaultSearchText();
            //BlockAuthenticatedUserActions();
            RightPanelExpandersInit();

            //Task.Run(() => 
            //{
            //    LoadWindow wnd = new LoadWindow(Settings.LoadImage)
            //    {
            //        WindowStartupLocation = WindowStartupLocation.CenterOwner,
            //        //Owner = this
            //    };
            //     wnd.ShowDialog();
            //});
            
        }

        private void BlockAuthenticatedUserActions()
        {
            btnArchive.Visibility = Visibility.Hidden;
            btnAdminPanel.Visibility = Visibility.Hidden;
            btnBasket.Visibility = Visibility.Hidden;
            rightPanelExpandersCard.Visibility = Visibility.Hidden;
        }

        private void RightPanelExpandersInit()
        {
            rightPanelExpanders.Add(expanderAddCategory);
            rightPanelExpanders.Add(expanderAddProductToBasket);
            rightPanelExpanders.Add(expanderSendProductToShop);
            rightPanelExpanders.Add(expanderWriteOffProduct);            
            rightPanelExpanders.Add(expanderDeleteProduct);                  
            rightPanelExpanders.Add(expanderAddProduct);
            rightPanelExpanders.Add(expanderEditProduct);
        }
        #endregion

        #region Search panel
        private void TbxSearch_GotMouseCapture(object sender, MouseEventArgs e)
        {
            tbxSearch.Text = string.Empty;
        }

        private void TbxSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            SetDefaultSearchText();
        }

        private void TbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!tbxSearch.Text.Equals(defaultSearchText) &&
                !tbxSearch.Text.Equals(string.Empty))
            {

                viewModel.TextBoxSearchText = tbxSearch.Text;
            }
        }
        #endregion

        #region Right panel Expanders

        #region Expanders Open/Close

        /*
            Remake MaterialDesigns's Expander

            When you click on an Expander, the previous Expander must be closed, if you click on the same Expander - this one must be closed too
         */

        private void ExpanderAddCategory_GotMouseCapture(object sender, MouseEventArgs e)
        {
            ExpanderHandler(expanderAddCategory);
        }

        private void ExpanderAddProductToBasket_GotMouseCapture(object sender, MouseEventArgs e)
        {
            ExpanderHandler(expanderAddProductToBasket);
        }

        private void ExpanderSendProductToStore_GotMouseCapture(object sender, MouseEventArgs e)
        {
            ExpanderHandler(expanderSendProductToShop);
        }
      
        private void ExpanderWriteOffProduct_GotMouseCapture(object sender, MouseEventArgs e)
        {
            ExpanderHandler(expanderWriteOffProduct);
        }

        private void ExpanderDeleteProduct_GotMouseCapture(object sender, MouseEventArgs e)
        {
            ExpanderHandler(expanderDeleteProduct);
        }

        private void ExpanderAddProduct_GotMouseCapture(object sender, MouseEventArgs e)
        {
            ExpanderHandler(expanderAddProduct);
        }

        private void ExpanderEditProduct_GotMouseCapture(object sender, MouseEventArgs e)
        {
            ExpanderHandler(expanderEditProduct);
        }

        private void ExpanderHandler(Expander currentExpander)
        {
            if (CanOpenExpanderManually(currentExpander))
            {
                currentExpander.IsExpanded = true;
            }
            if (!IsRepeatedExpanderPressing(currentExpander))
            {
                ClosePreviousOpendExpander();
            }
        }

        private void ClosePreviousOpendExpander()
        {
            int previousOpenedExpanderIdx = GetIdxOpenedExpander();
            if (previousOpenedExpanderIdx != -1)
            {
                var previousOpenedExpander = rightPanelExpanders[previousOpenedExpanderIdx];
                previousOpenedExpander.IsExpanded = false;
            }
        }

        private bool CanOpenExpanderManually(Expander currentExpander)
        {
            int currentExpanderIdx = rightPanelExpanders.IndexOf(currentExpander);
            int previousOpenedExpanderIdx = GetIdxOpenedExpander();

            return GetIdxOpenedExpander() != -1 && currentExpanderIdx > previousOpenedExpanderIdx;
        }
        private bool IsRepeatedExpanderPressing(Expander currentExpander)
        {
            int currentExpanderIdx = rightPanelExpanders.IndexOf(currentExpander);
            int previousOpenedExpanderIdx = GetIdxOpenedExpander();

            return currentExpanderIdx == previousOpenedExpanderIdx;
        }

        private int GetIdxOpenedExpander()
        {
            foreach (var expander in rightPanelExpanders)
            {
                if (expander.IsExpanded)
                {
                    return rightPanelExpanders.IndexOf(expander);
                }
            }
            return -1;
        }

        #endregion

        #region Add product, Edit product, add category
        private void BtnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            tbAddCategory.Text = string.Empty;
        }

        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductEditor wnd = new ProductEditor(ProductEditor.Action.Add)
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this
            };
            if (wnd.ShowDialog() == true)
            {
                viewModel.UpdateProducts(/*wnd.FilledProduct*/);
            }
        }

        private void ViewModel_StartEditProductEvent(int productId)
        {
            ProductEditor wnd = new ProductEditor(ProductEditor.Action.Edit, productId)
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this
            };
            if (wnd.ShowDialog() == true)
            {
                //wnd.FilledProduct.SelectionLabel = "Selected";
                //viewModel.ListBoxSelectedProduct = null;
                //viewModel.UpdateSelectedProduct(wnd.FilledProduct);
                viewModel.ListBoxSelectedProduct = null;
                viewModel.UpdateProducts(/*wnd.FilledProduct*/);
                //UpdateProducts(wnd);
            }
        }

        //private async void UpdateProducts(ProductEditor wnd)
        //{
            
        //    await 
        //}


        private void CbShops_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnWriteOff_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddToShop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddToBasket_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #endregion

        #region Login +
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(btnLogin.Content.ToString() == "Login")
            {
                UserDataInput wnd = new UserDataInput(UserDataInput.Action.Login)
                {
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = this
                };
                if (wnd.ShowDialog() == true)
                {
                    AnblockAuthenticatedUserActions();
                    btnLogin.Content = "Log out";

                    if (wnd.User.IsAdmin)
                    {
                        btnAdminPanel.Visibility = Visibility.Visible;
                    }
                }
            }
            else if(btnLogin.Content.ToString() == "Log out")
            {
                BlockAuthenticatedUserActions();
                btnLogin.Content = "Login";
            }
        }

        private void AnblockAuthenticatedUserActions()
        {
            btnArchive.Visibility = Visibility.Visible;
            btnBasket.Visibility = Visibility.Visible;
            rightPanelExpandersCard.Visibility = Visibility.Visible;
        }
        #endregion

        #region Top panel Buttons

        #region sorting

        #endregion

        #region Authenticated User Actions
        private void BtnAdminPanel_Click(object sender, RoutedEventArgs e)
        {
            AdminPanel wnd = new AdminPanel()
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this
            };
            wnd.ShowDialog();
        }

        private void BtnArchive_Click(object sender, RoutedEventArgs e)
        {
            Archive wnd = new Archive()
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this
            };
            wnd.ShowDialog();
        }

        private void BtnBasket_Click(object sender, RoutedEventArgs e)
        {
            Basket wnd = new Basket()
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this
            };
            wnd.ShowDialog();
        }
        #endregion

        #endregion

        #region Main ListBox Products

        // ShowProductButton
        private void BtnProductInfo_Click(object sender, RoutedEventArgs e)
        {
            ShowProduct wnd = new ShowProduct((lbProducts.SelectedItem as ProductUI).Id)
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this
            };
            wnd.ShowDialog();
        }

        private void LbProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            previousSelectedProduct.SelectionLabel = string.Empty;

            ProductUI product = lbProducts.SelectedItem as ProductUI;
            product.SelectionLabel = "Selected";
            previousSelectedProduct = product;
            viewModel.ListBoxSelectedProduct = product;
        }
        #endregion

        private void SetDefaultSearchText() => tbxSearch.Text = defaultSearchText;

       
    }
}
