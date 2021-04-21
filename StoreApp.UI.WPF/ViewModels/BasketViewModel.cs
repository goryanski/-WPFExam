using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
        private ProcessCommand _orderAllCommand;

        public ProcessCommand OrderAllCommand => _orderAllCommand ?? (_orderAllCommand = new ProcessCommand(obj =>
        {
            if(GroupedOrders.Count > 0)
            {

            }
            //RunAction(RIghtPanelActions.Delele);
        }/*, obj => obj != null*/));
        #endregion

        #region Delete Item
        public event Action OrderDeletionCompletedEvent;
        private ProcessCommand _deleteItemCommand;

        public ProcessCommand DeleteItemCommand => _deleteItemCommand ?? (_deleteItemCommand = new ProcessCommand(obj =>
        {
            OrderUI groupedOrder = obj as OrderUI;
            DeleteOrder(groupedOrder);
            
        }, obj => obj != null));

        private async void DeleteOrder(OrderUI groupedOrder)
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
            GroupedOrders.Remove(groupedOrder);

            // return product to warehouse
            await services.ProductsMapService.ReturnProductToWarehouse(productIdToReturn, groupedOrder.CountToOrder);
            OrderDeletionCompletedEvent?.Invoke();
        }
        #endregion





        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
