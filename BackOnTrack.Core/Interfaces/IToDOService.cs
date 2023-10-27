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
        public ToDo CreateToDo(ToDo toDo);
        public ToDo UpdateToDo(ToDo toDo);
        public ToDo DeleteToDo(ToDo toDo);
        public List<ToDo> GetAllToDos();
        public ToDo GetToDoById(int id);
        public ToDo GetToDoByName(string name);

    }
}
