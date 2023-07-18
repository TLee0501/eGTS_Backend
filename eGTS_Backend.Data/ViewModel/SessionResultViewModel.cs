using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class SessionResultViewModel
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }

        public string Result { get; set; } = null!;

        public bool IsDelete { get; set; }
    }
}
