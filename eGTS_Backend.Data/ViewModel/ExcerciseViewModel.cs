﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class ExcerciseViewModel
    {
        public Guid id { get; set; }
        public Guid Ptid { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Video { get; set; }
        public int CalorieCumsumption { get; set; }
        public int RepTime { get; set; }
        public string UnitOfMeasurement { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
