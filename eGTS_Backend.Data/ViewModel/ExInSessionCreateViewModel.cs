using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class ExInSessionCreateViewModel
    {
        public Guid SessionId { get; set; }

        public Guid ExerciseId { get; set; }
        public object ScheduleId { get; set; }
    }
}
