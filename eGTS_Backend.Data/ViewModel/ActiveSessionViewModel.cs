using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class ActiveSessionsViewModel
    {

        public Guid ScheduleID { get; set; }
        public List<SessionViewModel> SessionList { get; set; }

    }
}
