using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class ExcerciseTypeViewModel
    {

        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public Guid Ptid { get; set; }

    }
}
