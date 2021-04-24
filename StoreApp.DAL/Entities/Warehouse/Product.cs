using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.DAL.Entities.Warehouse
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public decimal PrimeCost { get; set; }
        public decimal Price { get; set; } // price for stores
        public bool IsAvailable { get; set; } // not sold, not wrote off, not deleted
        public DateTime ArrivalDate { get; set; }
        public DateTime SellBy { get; set; }
        public int AmountInStorage { get; set; }
        public int Rating { get; set; }
        public string PhotoPath { get; set; }
        public string SelectionLabel { get; set; }
        public int CountToOrder { get; set; }
        public int ShopId { get; set; }


        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public Provisioner Provisioner { get; set; }
        public int ProvisionerId { get; set; }
        public WarehouseSection Section { get; set; }
        public int SectionId { get; set; }
    }
}
