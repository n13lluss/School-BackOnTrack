using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOnTrack.Core.Models
{
    public class StressResult
    { 
        public int Id { get; set; }
        public int StressLevel { get; set; }
        public DateTime date { get; set; }
        public string UserId { get; set; }

    }
}
