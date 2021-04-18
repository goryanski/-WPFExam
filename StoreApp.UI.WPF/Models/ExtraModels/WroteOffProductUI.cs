using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.UI.WPF.Models.ExtraModels
{
    public class WroteOffProductUI : BaseModel
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
