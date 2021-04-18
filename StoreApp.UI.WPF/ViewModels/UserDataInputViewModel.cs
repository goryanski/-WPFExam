using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using StoreApp.BLL.DTO;
using StoreApp.UI.WPF.Commands;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Helpers.Validators;
using StoreApp.UI.WPF.Models;

namespace StoreApp.UI.WPF.ViewModels
{
    public class UserDataInputViewModel
    {
        public enum Command
        {
            Login,
            AddUser,
            UserDataChange
        }
        public Command CurrentCommand { get; set; }

        MapServicesStorage services = MapServicesStorage.Instance;
        UserValidator validator = new UserValidator(); 
        private ProcessCommand _actionCommand;


        public event Action<UserUI> CommandCompletedSuccessfullyEvent;
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
      

        public ProcessCommand ActionCommand => _actionCommand ?? (_actionCommand = new ProcessCommand(obj => 
        {
            UserUI user = new UserUI
            {
                Login = UserLogin,
                Password = UserPassword
            };

            switch (CurrentCommand)
            {
                case Command.Login:
                    LoginCommand(user);
                    break;
                case Command.AddUser:
                    AddUserCommand(user);
                    break;
                case Command.UserDataChange:
                    UserDataChangeCommand(user);
                    break;
            }      
        }));

        #region Login command
        private async void LoginCommand(UserUI user)
        {
            if (await UserExist(user))
            {
                if (!await IsPasswordCorrect(user))
                {
                    MessageBox.Show("Wrong password", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("User not found", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            user.IsAdmin = await IsUserAdmin(user);
            CommandCompletedSuccessfullyEvent?.Invoke(user);
        }

        internal async Task<bool> UserExist(UserUI user)
        {
            UserUI srchUser = await services.UsersMapService.GetUserByLogin(user.Login);
            return (srchUser is null) ? false : true;
        }

        internal async Task<bool> IsPasswordCorrect(UserUI user)
        {
            UserUI srchUser = await services.UsersMapService.GetUserByLogin(user.Login);
            return (srchUser.Password == user.Password) ? true : false;
        }
        #endregion

        #region Add/Change user
        private void AddUserCommand(UserUI user)
        {
            ConveyUserToAdminPanel(user);
        }

        private void UserDataChangeCommand(UserUI user)
        {
            ConveyUserToAdminPanel(user);
        }

        private async void ConveyUserToAdminPanel(UserUI user)
        {
            if (validator.IsUserValid(user))
            {
                user.IsAdmin = await IsUserAdmin(user);
                CommandCompletedSuccessfullyEvent?.Invoke(user);
            }
        }
        #endregion

        private async Task<bool> IsUserAdmin(UserUI user)
        {
            return await services.UsersMapService.IsUserAdmin(user);
        }
    }
}
