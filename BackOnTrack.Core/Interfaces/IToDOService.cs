﻿using BackOnTrack.Core.Models;
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
        public List<ToDo> GetAllToDos(string userID);
        public ToDo GetToDoById(int id);
        public List<ToDo> GetToDoByName(string name, string userId);
        public ToDo GetToDoByNameOnDate(string name, string userId, DateTime ondate);
        public List<ToDo> GetToDoByDate(DateTime date);

    }
}
