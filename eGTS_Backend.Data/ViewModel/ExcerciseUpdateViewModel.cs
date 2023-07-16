using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class ExcerciseUpdateViewModel
    {

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? Video { get; set; }
        public bool IsDelete { get; set; }
    }
}
