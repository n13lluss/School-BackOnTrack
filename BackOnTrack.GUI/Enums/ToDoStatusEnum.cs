using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOnTrack.GUI.Enums
{
    public enum ToDoStatus
    {
        [Description("Not Yet Started")]
        NotYetStarted = 0,
        [Description("Working on it")]
        WorkingOnIt = 1,
        [Description("Finished")]
        Finished = 2
    }
}