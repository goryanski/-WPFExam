using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.BLL.DTO.ExtraTables
{
    public class SoldProductDTO : BaseDTO
    {
        public int ProductId { get; set; }
        public int ShopId { get; set; }
        public int Amount { get; set; }
        public DateTime SoldDate { get; set; }
    }
}
