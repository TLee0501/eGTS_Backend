using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class RequestUpdateViewModel
    {
        public Guid Id { get; set; }
        public bool? IsAccepted { get; set; }
    }
}
