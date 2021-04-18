using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.BLL.DTO.ExtraTables
{
    public class WroteOffProductDTO : BaseDTO
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
