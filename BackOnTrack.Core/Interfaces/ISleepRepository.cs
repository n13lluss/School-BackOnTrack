using BackOnTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOnTrack.Core.Interfaces
{
    public interface ISleepRepository
    {
        public bool CreateResult(SleepResult result);
        public bool DeleteResult(SleepResult result);
        public bool UpdateResult(SleepResult result);
        public List<SleepResult> GetAll();
        public List<SleepResult> GetLastSeven();
        public SleepResult GetById(int id);
        public SleepResult GetResultByDateAndUserId(DateTime inputDate, string userId);
    }
}
