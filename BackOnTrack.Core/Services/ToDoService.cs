using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;

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
            try
            {
                toDo.Description ??= string.Empty;
                return _toDoRepository.CreateToDo(toDo);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when creating a ToDo item. Check the input or the database operation.", ex);
            }
        }

        public bool DeleteToDo(ToDo toDo)
        {
            try
            {
                return _toDoRepository.DeleteToDo(toDo);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when deleting a ToDo item. Check the input or the database operation.", ex);
            }
        }

        public List<ToDo> GetAllToDos(string Username)
        {
            try
            {
                return _toDoRepository.GetAllToDos(Username);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to retrieve the list of ToDo items.", ex);
            }
        }

        public List<ToDo> GetToDoByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public ToDo GetToDoById(int id)
        {
            try
            {
                return _toDoRepository.GetToDoById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "An error occurred when trying to get a ToDo item.");
                return new ToDo();
            }
        }

        public List<ToDo> GetToDoByName(string name, string userId)
        {
            try
            {
                return _toDoRepository.GetToDoByName(name, userId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when retrieving ToDo items by name and user ID.", ex);
            }
        }

        public ToDo GetToDoByNameOnDate(string name, string userId, DateTime ondate)
        {
            try
            {
                return _toDoRepository.GetToDoByNameOnDate(name, userId, ondate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "An error occurred when trying to get a ToDo item by name and date.");
                return new ToDo();
            }
        }

        public ToDo UpdateStatus(int id)
        {
            try
            {
                var result = _toDoRepository.GetToDoById(id);
                if (result.Status >= 2)
                {
                    if (_toDoRepository.UpdateStatus(id, 0))
                    {
                        result.Status = 0;
                        return result;
                    }
                }
                if (_toDoRepository.UpdateStatus(id, result.Status + 1))
                {
                    result.Status++;
                    return result;
                }

                ToDo toDo = new ToDo();
                return toDo;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when updating the status of a ToDo item.", ex);
            }
        }

        public bool UpdateToDo(ToDo toDo)
        {
            try
            {
                toDo.Description ??= string.Empty;
                return _toDoRepository.UpdateToDo(toDo);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when updating a ToDo item. Check the input or the database operation.", ex);
            }
        }
    }
}
