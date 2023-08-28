using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS_Backend.Data.ViewModel
{
    public class FeedbackUpdateViewModel
    {
        public short Rate { get; set; }
        public string Feedback1 { get; set; } = null!;
    }
}
