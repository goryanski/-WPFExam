using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using StoreApp.DAL;
using StoreApp.UI.WPF.Commands;
using StoreApp.UI.WPF.Extensions;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Models;

namespace StoreApp.UI.WPF.ViewModels
{
    public class BasketViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<OrderUI> DbOrders { get; set; } = new ObservableCollection<OrderUI>();
        public ObservableCollection<OrderUI> GroupedOrders { get; set; } = new ObservableCollection<OrderUI>();

        MapServicesStorage services = MapServicesStorage.Instance;

        public void Start()
        {
            InitOrdersList();
        }
        private async void InitOrdersList()
        {
            var orders = await services.OrdersMapService.GetAllOrders();

            // real data from DB
            DbOrders.AddRange(orders);

            // grouped orders only for UI (group by name to see total amount of ordered products)
            var groupedOrders = orders.GroupBy(o => o.ProductName, o => o.CountToOrder)
                .Select(o => new OrderUI
                {
                    ProductName = o.Key,
                    CountToOrder = o.Sum()
                })
                .ToList();

            GroupedOrders.AddRange(groupedOrders);
        }

        #region Selected ListBox order
        OrderUI _selectedOrder;
        public OrderUI SelectedListBoxOrder
        {
            get => _selectedOrder;
            set
            {
                if (value != _selectedOrder)
                {
                    _selectedOrder = value;
                    OnPropertyChanged(nameof(SelectedListBoxOrder));
                }
            }
        }
        #endregion

        #region Order all
        public event Action StartOrderAllEvent;

        private ProcessCommand _orderAllCommand;

        public ProcessCommand OrderAllCommand => _orderAllCommand ?? (_orderAllCommand = new ProcessCommand(obj =>
        {
            if(GroupedOrders.Count > 0)
            {
                StartOrderAllEvent?.Invoke();

                if (!Directory.Exists(Settings.OrdersDirectoryFolder))
                {
                    Directory.CreateDirectory(Settings.OrdersDirectoryFolder);
                }

                var prov = DbOrders.Select(o => o.ProvisionerId).ToList();
                var unic = prov.Distinct().ToList();

                //Process.Start("explorer.exe", Settings.OrdersDirectoryFolder);
                
                NewMethod(unic);
            }
            else
            {
                MessageBox.Show("There're no products", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }));

        private async void NewMethod(List<int> unic)
        {
            List<ProvisionerUI> provisioners = new List<ProvisionerUI>();
            foreach (var id in unic)
            {
                provisioners.Add(await services.ProvisionersMapService.GetProvisionerById(id));
            }

            foreach (var provisioner in provisioners)
            {
                string filename = $"{provisioner.Name}_{Guid.NewGuid()}.txt";
                string savePath = Path.GetFullPath(Path.Combine(Settings.OrdersDirectoryFolder, filename));
               
                foreach (var order in DbOrders)
                {
                    if(order.ProvisionerId == provisioner.Id)
                    {
                        await File.AppendAllTextAsync(savePath, $"{order.ProductName} [{order.CountToOrder}]\n");
                    }
                }
            }
            //////////////Process.Start("explorer.exe", Settings.OrdersDirectoryFolder);

            // delete orders from db 
            //var ordersToDelete = GroupedOrders;
            foreach (var order in GroupedOrders)
            {
                await DeleteOrder(order, false);
            }

            // // delete orders from collections
            DbOrders.Clear();
            GroupedOrders.Clear(); // убрать мусор и конец, потом делаем  btn open folder
        }
        #endregion

        #region Delete Item
        public event Action OrderDeletionCompletedEvent;
        private ProcessCommand _deleteItemCommand;

        public ProcessCommand DeleteItemCommand => _deleteItemCommand ?? (_deleteItemCommand = new ProcessCommand(obj =>
        {
            OrderUI groupedOrder = obj as OrderUI;
            DeleteOrderByCommand(groupedOrder, true);
            //DeleteOrder(groupedOrder, true);

        }, obj => obj != null));

        private async void DeleteOrderByCommand(OrderUI groupedOrder, bool v)
        {
           await DeleteOrder(groupedOrder, true);
        }

        private async Task DeleteOrder(OrderUI groupedOrder, bool withRestore)
        {
            int productIdToReturn = -1;

            // Delete from orders table (use ObservableCollection with real data from DB)
            foreach (var item in DbOrders)
            {
                if (item.ProductName.Equals(groupedOrder.ProductName))
                {
                    await services.OrdersMapService.DeleteOrder(item.Id);
                    productIdToReturn = item.OrderedProdictId;
                }
            }


            if (withRestore)
            {
                GroupedOrders.Remove(groupedOrder);

                // return product to warehouse
                await services.ProductsMapService.ReturnProductToWarehouse(productIdToReturn, groupedOrder.CountToOrder);
                OrderDeletionCompletedEvent?.Invoke();
            }
        }
        #endregion





        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
