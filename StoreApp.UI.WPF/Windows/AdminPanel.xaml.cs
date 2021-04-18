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
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        AdminPanelViewModel viewModel = new AdminPanelViewModel();
        public AdminPanel()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            UserDataInput wnd = new UserDataInput(UserDataInput.Action.AddUser)
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this
            };
            if (wnd.ShowDialog() == true)
            {
                viewModel.UserToAdd = wnd.User;
            }
        }

        private void BtnChangeUserData_Click(object sender, RoutedEventArgs e)
        {
            if(lbUsers.SelectedIndex != -1)
            {
                UserDataInput wnd = new UserDataInput(UserDataInput.Action.AddUser)
                {
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = this
                };
                if (wnd.ShowDialog() == true)
                {
                    viewModel.UserToEdit = wnd.User;
                }
            }
            else
            {
                MessageBox.Show("User not selected!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
