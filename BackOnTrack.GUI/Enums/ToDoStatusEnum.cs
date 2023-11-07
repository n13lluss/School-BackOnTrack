using System.ComponentModel;

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