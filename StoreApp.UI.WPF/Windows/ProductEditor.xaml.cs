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
using StoreApp.UI.WPF.Models;
using StoreApp.UI.WPF.Models.Warehouse;
using StoreApp.UI.WPF.ViewModels;

namespace StoreApp.UI.WPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditProduct.xaml
    /// </summary>
    public partial class ProductEditor : Window
    {
       // public ProductUI FilledProduct { get; set; }
        public enum Action
        {
            Edit,
            Add
        }

        public Action Act { get; set; }
        ProductEditorViewModel viewModel = new ProductEditorViewModel();

        public ProductEditor(Action action, int productId = -1)
        {
            InitializeComponent();
            //DataContext = new ProductEditorViewModel(productId);
            DataContext = viewModel;
            viewModel.SelectedProductId = productId;
            
            viewModel.OperationCompleteEvent += ViewModel_OperationCompleteEvent;
            Act = action;
            SetTitle();
           
        }

        private void ViewModel_OperationCompleteEvent(/*ProductUI product*/)
        {
            //FilledProduct = product;
            DialogResult = true;
            Close();
        }

        private void SetTitle()
        {
            switch (Act)
            {
                case Action.Edit:
                    Title = "Form of redaction";
                    break;
                case Action.Add:
                    Title = "Form of addition";
                    break;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            tbProvisionerMail.Text = tbProvisionerName.Text = string.Empty;
        }
    }
}
