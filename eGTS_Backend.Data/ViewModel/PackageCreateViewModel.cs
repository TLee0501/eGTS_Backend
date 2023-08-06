using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class PackageCreateViewModel
    {
        public string Name { get; set; } = null!;
        public bool HasPt { get; set; }
        public bool HasNe { get; set; }
        public short? NumberOfsession { get; set; }
        public short? NumberOfMonth { get; set; }
        public double? Ptcost { get; set; }
        public double? Necost { get; set; }
        public double? CenterCost { get; set; }
        public double? Discount { get; set; }
        public double Price { get; set; }
    }
}
