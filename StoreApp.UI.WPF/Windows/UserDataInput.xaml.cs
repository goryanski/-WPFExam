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
using StoreApp.BLL.DTO;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Models;
using StoreApp.UI.WPF.ViewModels;

namespace StoreApp.UI.WPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для Authentication.xaml
    /// </summary>
    public partial class UserDataInput : Window
    {
        public enum Action
        {
            Login,
            AddUser,
            UserDataChange
        }
        public Action Act { get; set; }
        UserDataInputViewModel viewModel = new UserDataInputViewModel();
        public UserUI User { get; set; }

        public UserDataInput(Action action)
        {
            InitializeComponent();
            Act = action;

            DataContext = viewModel;
            viewModel.CommandCompletedSuccessfullyEvent += AuthenticationViewModel_CommandCompletedSuccessfully;
            viewModel.CurrentCommand = GetCommand();

            SetTitle();
        }

        #region Load
        private UserDataInputViewModel.Command GetCommand()
        {
            return Act switch
            {
                Action.Login => UserDataInputViewModel.Command.Login,
                Action.AddUser => UserDataInputViewModel.Command.AddUser,
                Action.UserDataChange => UserDataInputViewModel.Command.UserDataChange,
                _ => UserDataInputViewModel.Command.Login,
            };
        }

        private void SetTitle()
        {
            switch (Act)
            {
                case Action.Login:
                    this.Title = "Login";
                    break;
                case Action.AddUser:
                    this.Title = "Add user";
                    break;
                case Action.UserDataChange:
                    this.Title = "User data change";
                    break;
            }
        }
        #endregion

        private void AuthenticationViewModel_CommandCompletedSuccessfully(UserUI user)
        {
            User = user;
            DialogResult = true;
            Close();
        }
       
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PbxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            viewModel.UserPassword = pbxPassword.Password;
        }
    }
}
