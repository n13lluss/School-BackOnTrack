using BackOnTrack.Core.Models;
using BackOnTrack.GUI.Models.ToDo;

namespace BackOnTrack.GUI.Models.Stress
{
    public class DetailsStressViewModel
    {
        public int Id { get; set; }
        public string Result { get; set; }
        public DateTime date { get; set; }
        public int SleptLastNight { get; set; }
        public int TasksPlanned { get; set; }
        public List<ToDoIndexViewModel> ToDos { get; set; }

    }
}
