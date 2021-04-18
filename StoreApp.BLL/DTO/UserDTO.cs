using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.BLL.DTO
{
    public class UserDTO : BaseDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string Label { get; set; }
    }
}
