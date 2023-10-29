using BackOnTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOnTrack.Core.Interfaces
{
    public interface IToDOService
    {
        public bool CreateToDo(ToDo toDo);
        public bool UpdateToDo(ToDo toDo);
        public bool DeleteToDo(ToDo toDo);
        public ToDo UpdateStatus(int id);
        public List<ToDo> GetAllToDos();
        public ToDo GetToDoById(int id);
        public ToDo GetToDoByName(string name);
        public List<ToDo> GetToDoByDate(DateTime date);

    }
}
