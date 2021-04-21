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
        public bool IsChangesInDb { get; set; }

        public Basket()
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.Start();
            viewModel.OrderDeletionCompletedEvent += ViewModel_OrderDeletionCompletedEvent;
            viewModel.StartOrderAllEvent += ViewModel_StartOrderAllEvent;
            IsChangesInDb = false;

            BlockButtons(true);
        }

        private void ViewModel_StartOrderAllEvent()
        {
            BlockButtons(false);
        }

        private void ViewModel_OrderDeletionCompletedEvent()
        {
            IsChangesInDb = true;
        }

        private void BlockButtons(bool isBlocking)
        {
            btnOpenFolder.IsEnabled = btnSendOrder.IsEnabled = !isBlocking;
            //if (isBlocking)
            //{
            //    btnOpenFolder.IsEnabled = btnSendOrder.IsEnabled = false;
            //}
            //else
            //{
            //    btnOpenFolder.IsEnabled = btnSendOrder.IsEnabled = true;
            //}
        }

        private void BtnOrderAll_Click(object sender, RoutedEventArgs e)
        {
            //
            
        }

        private void BtnSendOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOrderAll_Click(object sender, RoutedEventArgs e)
        {

        }

       
    }
}
