using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class SessionUpdateViewModel
    {
        public DateTime DateTime { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public List<Guid>? ListExcercise { get; set; }
    }
}
