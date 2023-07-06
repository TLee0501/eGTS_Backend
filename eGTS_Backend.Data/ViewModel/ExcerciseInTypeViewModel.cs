using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class ExcerciseInTypeViewModel
    {
        public Guid Id { get; set; }

        public Guid ExerciseTypeId { get; set; }

        public Guid ExerciseId { get; set; }
    }
}
