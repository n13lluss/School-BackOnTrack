using BackOnTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOnTrack.Core.Interfaces
{
    public interface IAccountService
    {
        public UserModel CorrectLogin(string UsernameOrEmail, string password);
        public bool CorrectRegistration(UserModel user);
        public UserModel GetUser(string UsernameOrEmail);
        public bool CreateUser(UserModel user);
    }
}
