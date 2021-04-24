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
    /// Логика взаимодействия для Archive.xaml
    /// </summary>
    public partial class Archive : Window
    {
        ArchiveViewModel viewModel = new ArchiveViewModel();
        public Archive()
        {
            InitializeComponent();
            DataContext = viewModel;
            ViewModelSubscriptions();
            viewModel.Start();
        }

        private void ViewModelSubscriptions()
        {
            viewModel.NoWroteOffProductsFoundEvent += ViewModel_NoWroteOffProductsFoundEvent;
            viewModel.SomeWroteOffProductsFoundEvent += ViewModel_SomeWroteOffProductsFoundEvent;
            viewModel.NoSoldProductsFoundEvent += ViewModel_NoSoldProductsFoundEvent;
            viewModel.SomeSoldProductsFoundEvent += ViewModel_SomeSoldProductsFoundEvent;
        }

        #region Products not found label - visible/hidden
        private void ViewModel_SomeSoldProductsFoundEvent()
        {
            tbSoldProductsNotFound.Visibility = Visibility.Hidden;
        }

        private void ViewModel_NoSoldProductsFoundEvent()
        {
            tbSoldProductsNotFound.Visibility = Visibility.Visible;
        }

        private void ViewModel_SomeWroteOffProductsFoundEvent()
        {
            tbWroteOffProductsNotFound.Visibility = Visibility.Hidden;
        }

        private void ViewModel_NoWroteOffProductsFoundEvent()
        {
            tbWroteOffProductsNotFound.Visibility = Visibility.Visible;
        }
        #endregion


        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
