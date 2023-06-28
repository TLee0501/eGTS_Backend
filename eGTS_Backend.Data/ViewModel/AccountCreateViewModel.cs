using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class AccountCreateViewModel
    {
        public string PhoneNo { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string FullName { get; set; } = null!;
        public DateTime CreateTime { get; set; }
    }
}
