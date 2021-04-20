using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace StoreApp.UI.WPF.Models.Warehouse
{
    public class ProductUI : BaseModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _selectionLabel = string.Empty;
        private string _photoPath = string.Empty;
        private int _count = 0;

        public string Name { get; set; }
        public double Weight { get; set; }
        public decimal PrimeCost { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime ArrivalDate { get; set; } 
        public DateTime SellBy { get; set; }

        public int AmountInStorage
        {
            get => _count;
            set
            {
                if (_count != value)
                {
                    _count = value;
                    OnPropertyChanged(nameof(AmountInStorage));
                }
            }
        }

        public int Rating { get; set; }
        public string PhotoPath
        {
            get => _photoPath;
            set
            {
                if (!_photoPath.Equals(value))
                {
                    _photoPath = value;
                    OnPropertyChanged(nameof(PhotoPath));
                }
            }
        }

        public string SelectionLabel
        {
            get => _selectionLabel;
            set
            {
                if (!_selectionLabel.Equals(value))
                {
                    _selectionLabel = value;
                    OnPropertyChanged(nameof(SelectionLabel));
                }
            }
        }


        public CategoryUI Category { get; set; }
        public int CategoryId { get; set; } 
        public ProvisionerUI Provisioner { get; set; }
        public int ProvisionerId { get; set; }
        public WarehouseSectionUI Section { get; set; }
        public int SectionId { get; set; }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
