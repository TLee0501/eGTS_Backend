using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class BodyPerameterCreateViewModel
    {

        public Guid GymerId { get; set; }

        public string? Goal { get; set; }

        public double? Weight { get; set; }

        public double? Height { get; set; }

        public double? Bmi { get; set; }

        public short? Bone { get; set; }

        public short? Fat { get; set; }

        public short? Muscle { get; set; }

    }
}
