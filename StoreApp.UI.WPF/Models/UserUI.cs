using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.UI.WPF.Models
{
    public class UserUI : BaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string Label { get; set; }
    }
}
