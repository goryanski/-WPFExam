using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StoreApp.UI.WPF.ViewModels;

namespace StoreApp.UI.WPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddNewShop.xaml
    /// </summary>
    public partial class AddNewShop : Window
    {
        AddNewShopViewModel viewModel = new AddNewShopViewModel();
        public bool IsChangesInDb { get; set; }

        public AddNewShop()
        {
            InitializeComponent();

            DataContext = viewModel;
            viewModel.ShopWasAddedEvent += ViewModel_ShopWasAddedEvent;
            IsChangesInDb = false;
        }

        private void ViewModel_ShopWasAddedEvent()
        {
            IsChangesInDb = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
