using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class ExScheduleUpdateViewModel
    {

        public Guid Ptid { get; set; }

        public string From { get; set; }

        public string To { get; set; }
        public bool IsDeleted { get; set; }

    }
}
