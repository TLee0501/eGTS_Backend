using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class SessionCreateViewModelV2
    {
        public Guid PackageGymerID { get; set; }
        public DateTime DateAndTime { get; set; }
        public List<Guid> ListExcerciseID { get; set; }
    }
}
