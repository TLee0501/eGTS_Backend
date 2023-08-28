using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class FeedbackViewModel
    {
        public Guid Id { get; set; }
        public Guid PtidorNeid { get; set; }
        public Guid PackageGymerId { get; set; }
        public string PackageName { get; set; }
        public string GymerName { get; set; }
        public string PTOrNeName { get; set; }
        public short Rate { get; set; }
        public string Feedback1 { get; set; } = null!;
        public bool IsDelete { get; set; }
    }
}
