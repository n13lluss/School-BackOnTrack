using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOnTrack.Core.Models
{
    public class SleepResult
    {
        public int Id { get; set; }
        public int HoursSlept { get; set; }
        public DateTime Date {  get; set; }
        public string UserID { get; set; }
    }
}
