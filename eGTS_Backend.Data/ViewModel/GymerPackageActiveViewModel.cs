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
        public Guid PackageGymerId { get; set; }
        public string PackageName { get; set; }
        public DateTime From { get; set; }
        public string Status { get; set; }
        public short? NumberOfSession { get; set; }
        public bool isUpdate { get; set; }
    }
}
