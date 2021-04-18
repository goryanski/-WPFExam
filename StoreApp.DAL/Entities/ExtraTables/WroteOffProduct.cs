using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.DAL.Entities.ExtraTables
{
    public class WroteOffProduct : BaseEntity
    { 
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
