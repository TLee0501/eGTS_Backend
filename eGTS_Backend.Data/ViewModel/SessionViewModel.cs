using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class SessionViewModel
    {
        public Guid id { get; set; }
        public Guid ScheduleId { get; set; }
        public DateTime DateAndTime { get; set; }
        public bool IsDelete { get; set; }
    }
}
