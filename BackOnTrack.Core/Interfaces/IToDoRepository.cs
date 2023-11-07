using BackOnTrack.Core.Models;

namespace BackOnTrack.Core.Interfaces
{
    public interface IToDoRepository
    {
        public bool CreateToDo(ToDo toDo);
        public bool UpdateToDo(ToDo toDo);
        public bool DeleteToDo(ToDo toDo);
        public bool UpdateStatus(int id, int status);
        public List<ToDo> GetAllToDos(string userId);
        public ToDo GetToDoById(int id);
        public List<ToDo> GetToDoByName(string name, string userId);
        public ToDo GetToDoByNameOnDate(string name, string userId, DateTime ondate);
        public List<ToDo> GetToDoByDate(DateTime date);
    }
}
