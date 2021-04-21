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
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket : Window
    {
        BasketViewModel viewModel = new BasketViewModel();
        public Basket()
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.Start();

            BlockButtons(true);
        }

        private void BlockButtons(bool isBlocking)
        {
            if (isBlocking)
            {
                btnOpenFolder.IsEnabled = btnSendOrder.IsEnabled = false;
            }
            else
            {
                btnOpenFolder.IsEnabled = btnSendOrder.IsEnabled = true;
            }
        }

        private void BtnOrderAll_Click(object sender, RoutedEventArgs e)
        {
            if (lbOrders.SelectedIndex != -1)
            {
                BlockButtons(false);
            }
            //else
            //{
            //    MessageBox.Show("Select product first", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
        }

        private void BtnSendOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOrderAll_Click(object sender, RoutedEventArgs e)
        {

        }

       
    }
}
