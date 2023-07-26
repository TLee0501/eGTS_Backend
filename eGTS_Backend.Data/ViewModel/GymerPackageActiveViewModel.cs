using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class GymerPackageActiveViewModel
    {
        public Guid GymerId { get; set; }
        public string GymerName { get; set; }
        public Guid PackageId { get; set; }
        public string PackageName { get; set; }
    }
}
