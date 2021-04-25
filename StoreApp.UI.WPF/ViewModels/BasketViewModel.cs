using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
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

        private ProcessCommand _orderAllCommand;

        public ProcessCommand OrderAllCommand => _orderAllCommand ?? (_orderAllCommand = new ProcessCommand(obj =>
        {
            if(GroupedOrders.Count > 0)
            {
                // directory for orders files
                if (!Directory.Exists(Settings.OrdersDirectoryFolder))
                {
                    Directory.CreateDirectory(Settings.OrdersDirectoryFolder);
                }


                // get id of provisioners to form orders files
                var provisionersId = DbOrders.Select(o => o.ProvisionerId).ToList();
                var unicIdProvisioners = provisionersId.Distinct().ToList();
                
                FormOrdersFiles(unicIdProvisioners);
            }
            else
            {
                MessageBox.Show("There're no products", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }));

        private async void FormOrdersFiles(List<int> unic)
        {
            List<ProvisionerUI> provisioners = new List<ProvisionerUI>();
            foreach (var id in unic)
            {
                provisioners.Add(await services.ProvisionersMapService.GetProvisionerById(id));
            }

            // sort orders by provisioners, create files to each provisioner, write suibable oreders inside them
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
           

            // delete orders from db 
            foreach (var order in GroupedOrders)
            {
                await DeleteOrder(order, true);
            }

            // delete orders from collections
            DbOrders.Clear();
            GroupedOrders.Clear(); 
        }
        #endregion
        
        #region Delete Item
        public event Action OrderDeletionCompletedEvent;
        private ProcessCommand _deleteItemCommand;

        public ProcessCommand DeleteItemCommand => _deleteItemCommand ?? (_deleteItemCommand = new ProcessCommand(obj =>
        {
            OrderUI groupedOrder = obj as OrderUI;
            DeleteOrderByCommand(groupedOrder, false);

        }, obj => obj != null));

        private async void DeleteOrderByCommand(OrderUI groupedOrder, bool deleteDuringOrder)
        {
           await DeleteOrder(groupedOrder, deleteDuringOrder);
        }

        private async Task DeleteOrder(OrderUI groupedOrder, bool deleteDuringOrder, bool withRestore = false)
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


            if (!deleteDuringOrder)
            {
                // for delete by pressed delete button
                GroupedOrders.Remove(groupedOrder);
            }


            // not used now, this is for the future when this app will work in conjunction with the website (if customer (store or supermarket) add product in basket on website, but then refuses and removes the order from basket - product must to return to warehouse db)
            if (withRestore)
            {
                GroupedOrders.Remove(groupedOrder);

                // return product to warehouse
                await services.ProductsMapService.ReturnProductToWarehouse(productIdToReturn, groupedOrder.CountToOrder);
                OrderDeletionCompletedEvent?.Invoke();
            }
        }
        #endregion

        #region Button open folder
        private ProcessCommand _openFolderCommand;
        public ProcessCommand OpenFolderCommand => _openFolderCommand ?? (_openFolderCommand = new ProcessCommand(obj =>
        {
            if (Directory.Exists(Settings.OrdersDirectoryFolder))
            {
                Process.Start("explorer.exe", Settings.OrdersDirectoryFolder);
            }
            else
            {
                MessageBox.Show("Directory with orders has been deleted", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }));
        #endregion

        #region Send Orders Command

        SendOrdersService sendService = new SendOrdersService();

        private ProcessCommand _sendOrdersCommand;
        public ProcessCommand SendOrdersCommand => _sendOrdersCommand ?? (_sendOrdersCommand = new ProcessCommand(obj =>
        {
            if (!Directory.Exists(Settings.OrdersDirectoryFolder))
            {
                MessageBox.Show("Directory with orders has been deleted", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SendOrders(Settings.OrdersDirectoryFolder);
        }));

        private async void SendOrders(string folderPath)
        {
            // take files from order files directory, sort them by provisioners and send each provisioner email to order products
            string[] allFiles = Directory.GetFiles(folderPath);
            if (allFiles.Length > 0)
            {
                var provisioners = await services.ProvisionersMapService.GetAllProvisioners();
                foreach (var provisioner in provisioners)
                {
                    // select files to send this provisioner
                    List<string> orderfiles = new List<string>();

                    foreach (var file in allFiles)
                    {
                        if (Path.GetFileName(file).Contains(provisioner.Name))
                        {
                            orderfiles.Add(file);
                        }
                    }

                    if(orderfiles.Count > 0)
                    {
                        // first move files to archive (avoid conflicts)
                        MoveFilesToArchive(folderPath, orderfiles.ToArray());


                        // change files path to archive folder path (because we moved them)
                        for (int i = 0; i < orderfiles.Count; i++)
                        {
                            orderfiles[i] = orderfiles[i].Replace(Settings.OrdersDirectoryFolder, Settings.OrdersDirectoryArchiveFolder);
                        }

                        try
                        {
                            // send emails to selected provisioner
                            await Send(provisioner.Mail, orderfiles.ToArray());
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show($"Unexpected error: you must send files to the provisioner " +
                                $"'{provisioner.Name}' manually " +
                                $"(directory: {Settings.OrdersDirectoryArchiveFolder}, names started with" +
                                $" {provisioner.Name}).\n" +
                                $"Error details: {ex.Message}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                MessageBox.Show("All orders have been sended to provisioners and moved to archive", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Nothing to order", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MoveFilesToArchive(string folderPath, string[] orderedFiles)
        {
            // check directory for archive files
            if (!Directory.Exists(Settings.OrdersDirectoryArchiveFolder))
            {
                Directory.CreateDirectory(Settings.OrdersDirectoryArchiveFolder);
            }

            foreach (var orderedFile in orderedFiles)
            {
                string destination = Path.Combine(Settings.OrdersDirectoryArchiveFolder, Path.GetFileName(orderedFile));

                File.Move(orderedFile, destination);
            }
        }

        private async Task Send(string toMail, string[] selectedFiles)
        {
            await sendService.SendMailMessage(
                    toMail
                    , "New order"
                    , "Orders:\n"
                    , false
                    , sendService.GetAttachments(selectedFiles)
             );
        }
        #endregion



        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
