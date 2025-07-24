using BusinessObject;
using Repositories.Implement;
using Repositories.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class UserService : IUserService
    {
        IUserRepository userRepository;
        public UserService()
        {
            userRepository = new UserRepository();
        }

        public void AddUser(User user)
        {
            var existingUser = userRepository.GetUserByUsername(user.Username);
            if (existingUser == null)
            {
                userRepository.AddUser(user);
            }
            else
            {
                throw new Exception("Username already exists");
            }
        }

        public void DeleteUser(string userId)
        {
            var existingUser = userRepository.GetUserById(userId);
            if (existingUser != null)
            {
                userRepository.DeleteUser(userId);
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public List<User> GetAllUsers()
        {
            return userRepository.GetAllUsers();
        }

        public User? GetUserById(string userId)
        {
            return userRepository.GetUserById(userId);
        }

        public User? GetUserByUsername(string username)
        {
            return userRepository.GetUserByUsername(username);
        }

        public User Login(string email, string password)
        {
            return userRepository.Login(email, password);
        }

        public void UpdateUser(User user)
        {
            var existingUser = userRepository.GetUserById(user.UserId);
            if (existingUser != null)
            {
                userRepository.UpdateUser(user);
            }
            else
            {
                throw new Exception("User not found");
            }
        }
    }
}
