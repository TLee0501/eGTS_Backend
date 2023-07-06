using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class PackageViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public bool HasPt { get; set; }

        public bool HasNe { get; set; }

        public short NumberOfsession { get; set; }

        public double Price { get; set; }
    }
}
