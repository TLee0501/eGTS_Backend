using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class SessionResultCreateViewModel
    {
        public Guid SessionId { get; set; }

        public string Result { get; set; } = null!;
    }
}
