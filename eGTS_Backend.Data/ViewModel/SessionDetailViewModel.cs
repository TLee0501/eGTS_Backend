using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class SessionDetailViewModel
    {
        public Guid id { get; set; }
        public Guid ScheduleId { get; set; }
        public DateTime DateAndTime { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<ExcerciseViewModel> Excercises { get; set; }
    }
}
