using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class FeedbackAverageViewModel
    {
        public Guid Id { get; set; }
        public Guid PtidorNeid { get; set; }
        public string PTOrNeName { get; set; }
        public double AverageRate { get; set; }
    }
}
