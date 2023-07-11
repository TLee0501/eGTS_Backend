using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class ExScheduleViewModel
    {

        public Guid GymerId { get; set; }

        public Guid Ptid { get; set; }

        public Guid PackageGymerId { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

    }
}
