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
            IsChangesInDb = false;
        }

        private void ViewModel_OrderDeletionCompletedEvent()
        {
            IsChangesInDb = true;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
