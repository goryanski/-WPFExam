using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.BLL.DTO.Warehouse
{
    public class ProductDTO : BaseDTO
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public decimal PrimeCost { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } 
        public DateTime ArrivalDate { get; set; }
        public DateTime SellBy { get; set; }
        public int AmountInStorage { get; set; }
        public int Rating { get; set; }
        public string PhotoPath { get; set; }
        public string SelectionLabel { get; set; }
        public int CountToOrder { get; set; }
        public int ShopId { get; set; }


        public CategoryDTO Category { get; set; }
        public int CategoryId { get; set; }
        public ProvisionerDTO Provisioner { get; set; }
        public int ProvisionerId { get; set; }
        public WarehouseSectionDTO Section { get; set; }
        public int SectionId { get; set; }
    }
}
