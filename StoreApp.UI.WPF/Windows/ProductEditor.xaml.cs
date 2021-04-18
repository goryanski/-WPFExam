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
using StoreApp.UI.WPF.ViewModels;

namespace StoreApp.UI.WPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditProduct.xaml
    /// </summary>
    public partial class ProductEditor : Window
    {
        public enum Action
        {
            Edit,
            Add
        }

        public Action Act { get; set; }
        //ProductEditorViewModel viewModel = new ProductEditorViewModel();

        public ProductEditor(Action action, int productId = 0)
        {
            InitializeComponent();
            DataContext = new ProductEditorViewModel(productId);
            Act = action;
            SetTitle();
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

        private void cbCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbSections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbSections_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbProvisioners_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
