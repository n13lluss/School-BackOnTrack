using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOnTrack.Test.Repositories
{
    public class MockToDoRepository : IToDoRepository
    {
        public bool CreateToDo(ToDo toDo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteToDo(ToDo toDo)
        {
            throw new NotImplementedException();
        }

        public List<ToDo> GetAllToDos(string userId)
        {
            throw new NotImplementedException();
        }

        public List<ToDo> GetToDoByDate(DateTime date, string userId)
        {
            throw new NotImplementedException();
        }

        public ToDo GetToDoById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ToDo> GetToDoByName(string name, string userId)
        {
            throw new NotImplementedException();
        }

        public ToDo GetToDoByNameOnDate(string name, string userId, DateTime ondate)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStatus(int id, int status)
        {
            throw new NotImplementedException();
        }

        public bool UpdateToDo(ToDo toDo)
        {
            throw new NotImplementedException();
        }
    }
}
