using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOnTrack.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository UserRepository)
        {
            _userRepository = UserRepository;
        }
        public UserModel CorrectLogin(string UsernameOrEmail, string password)
        {
            List<UserModel> users = _userRepository.GetAll();
            foreach (UserModel user in users)
            {
                if((user.Username == UsernameOrEmail || user.Email == UsernameOrEmail) && user.Password == password)
                {
                    return user;
                }
            }
            return null;

        }

        public bool CorrectRegistration(UserModel user)
        {
            List<UserModel> users = _userRepository.GetAll();
            foreach(UserModel Existinguser in users)
            {
                if(Existinguser.Username == user.Username || Existinguser.Email == user.Email)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CreateUser(UserModel user)
        {
            bool succeeded = _userRepository.CreateUser(user);
            return succeeded;
        }

        public UserModel GetUser(string UsernameOrEmail)
        {
            List<UserModel> users = _userRepository.GetAll();
            foreach (UserModel user in users)
            {
                if(user.Username == UsernameOrEmail || user.Email== UsernameOrEmail)
                {
                    return user;
                }
            }
            throw new NotImplementedException();
        }
    }
}
