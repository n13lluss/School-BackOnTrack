using BackOnTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOnTrack.Core.Interfaces
{
    public interface IUserRepository
    {
        public List<UserModel> GetAll();
        public UserModel GetById(int id);
        public bool CreateUser(UserModel user);
    }
}
