using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class PackageCreateViewModel
    {
        public bool HasPt { get; set; }

        public bool HasNe { get; set; }

        public short NumberOfsession { get; set; }

        public double Price { get; set; }
    }
}
