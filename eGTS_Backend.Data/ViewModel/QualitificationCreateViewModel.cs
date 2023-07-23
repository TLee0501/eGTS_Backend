using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class QualitificationCreateViewModel
    {
        public Guid ExpertId { get; set; }
        public string? Certificate { get; set; }
        public short? Experience { get; set; }
    }
}
