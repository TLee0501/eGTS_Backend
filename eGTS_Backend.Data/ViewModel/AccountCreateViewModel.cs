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

        public string Image { get; set; } = null!;

        public string Fullname { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public string Role { get; set; } = null!;

        public DateTime CreateDate { get; set; }

        public bool IsDelete { get; set; }
    }
}
