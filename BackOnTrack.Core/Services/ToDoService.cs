using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOnTrack.Core.Services
{
    public class ToDoService : IToDOService
    {
        private readonly IToDoRepository _toDoRepository;

        public ToDoService(IToDoRepository ToDoRepository)
        {
            _toDoRepository = ToDoRepository;
        }
        public bool CreateToDo(ToDo toDo)
        {
            toDo.Description ??= string.Empty;
            return _toDoRepository.CreateToDo(toDo);
        }

        public bool DeleteToDo(ToDo toDo)
        {
            return _toDoRepository.DeleteToDo(toDo);
        }

        public List<ToDo> GetAllToDos(string Username)
        {
            return _toDoRepository.GetAllToDos(Username);
        }

        public List<ToDo> GetToDoByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public ToDo GetToDoById(int id)
        {
            return _toDoRepository.GetToDoById(id);
        }

        public List<ToDo> GetToDoByName(string name, string userId)
        {
            return _toDoRepository.GetToDoByName(name, userId);
        }

        public ToDo GetToDoByNameOnDate(string name, string userId, DateTime ondate)
        {
            return _toDoRepository.GetToDoByNameOnDate(name, userId, ondate);
        }

        public ToDo UpdateStatus(int id)
        {
            var result = _toDoRepository.GetToDoById(id);
            if(result.Status >= 2)
            {
                if (_toDoRepository.UpdateStatus(id, 0))
                {
                    result.Status = 0;
                    return result;
                }
            }
            if (_toDoRepository.UpdateStatus(id, result.Status + 1)) {
                result.Status++;
                return result;
            };
            
            ToDo toDo = new ToDo();
            return toDo;
        }

        public bool UpdateToDo(ToDo toDo)
        {
            toDo.Description ??= string.Empty;
            return _toDoRepository.UpdateToDo(toDo);
        }
    }
}
