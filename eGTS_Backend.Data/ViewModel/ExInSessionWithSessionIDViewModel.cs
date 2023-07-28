using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class ExInSessionWithSessionIDViewModel
    {
        public Guid SessionID { get; set; }
        public DateTime SessionDateAndTime { get; set; }
        public List<ExcerciseViewModel> ExcercisesInSession { get; set; }


    }
}
