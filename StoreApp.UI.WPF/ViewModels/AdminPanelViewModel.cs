using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using StoreApp.UI.WPF.Commands;
using StoreApp.UI.WPF.Extensions;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Models;

namespace StoreApp.UI.WPF.ViewModels
{
    public class AdminPanelViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        MapServicesStorage services = MapServicesStorage.Instance;
        public ObservableCollection<UserUI> Users { get; set; } = new ObservableCollection<UserUI>();

        public AdminPanelViewModel( )
        {
            DataInit();
        }

        private async void DataInit()
        {
            Users.AddRange(await services.UsersMapService.GetAllUsers());
        }

        #region Selected ListBox user
        UserUI _selectedUser;
        public UserUI SelectedListBoxUser
        {
            get => _selectedUser;
            set
            {
                if (value != _selectedUser)
                {
                    _selectedUser = value;
                    OnPropertyChanged(nameof(SelectedListBoxUser));
                }
            }
        }
        #endregion

        #region Add/Edit ObservableCollection<UserUI> Users

        //Add
        UserUI _userToAdd;
        public UserUI UserToAdd
        {
            get => _userToAdd;
            set
            {
                if (value != _userToAdd)
                {
                    _userToAdd = value;
                    CreateUser(_userToAdd);
                }
            }
        }

        private async void CreateUser(UserUI newUser)
        {
            await services.UsersMapService.CreateUser(newUser);

            // get this user id from DB to add to ObservableCollection user with id
            newUser.Id = await services.UsersMapService.GetUserId(newUser);
            Users.Add(newUser);
        }



        //Edit
        UserUI _userToEdit;
        public UserUI UserToEdit
        {
            get => _userToEdit;
            set
            {
                if (value != _userToEdit)
                {
                    _userToEdit = value;
                    EditSelectedUser(_userToEdit);
                }
            }
        }

        private void EditSelectedUser(UserUI userToEdit)
        {
            // fill in missing fields
            userToEdit.Id = SelectedListBoxUser.Id;
            userToEdit.IsAdmin = SelectedListBoxUser.IsAdmin;
            userToEdit.Label = SelectedListBoxUser.Label;

            // update in DB
            services.UsersMapService.EditUser(userToEdit);

            // update ObservableCollection
            int idx = Users.IndexOf(SelectedListBoxUser);
            Users.RemoveAt(idx);
            Users.Insert(idx, userToEdit);
        }
        #endregion

        #region Delete user

        private ProcessCommand _removeCommand;
        public ProcessCommand RemoveCommand => _removeCommand ?? (_removeCommand = new ProcessCommand(obj =>
        {
            UserUI user = obj as UserUI;
            RemoveSelectedUser(user);

        }, obj => obj != null));


        private async void RemoveSelectedUser(UserUI user)
        {
            if(await services.UsersMapService.IsUserAdmin(user))
            {
                MessageBox.Show("You cannot delete yourself", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            services.UsersMapService.RemoveUser(user);
            Users.Remove(user);
        }
        #endregion

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
