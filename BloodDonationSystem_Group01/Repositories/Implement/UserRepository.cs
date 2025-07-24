using BusinessObject;
using DataAccessLayer;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class UserRepository : IUserRepository
    {
        UserDAO UserDAO = new UserDAO();

        public void AddUser(User user)
        {
            UserDAO.AddUser(user);
        }

        public void DeleteUser(string userId)
        {
            UserDAO.DeleteUser(userId);
        }

        public List<User> GetAllUsers()
        {
            return UserDAO.GetAllUsers();
        }

        public User? GetUserById(string userId)
        {
            return UserDAO.GetUserById(userId);
        }

        public User? GetUserByUsername(string username)
        {
            return UserDAO.GetUserByUsername(username);
        }

        public User Login(string email, string password)
        {
            return UserDAO.Login(email, password);
        }

        public void UpdateUser(User user)
        {
            var existingUser = UserDAO.GetUserById(user.UserId);
            if (existingUser != null)
            {
                UserDAO.UpdateUser(user);
            }
            else
            {
                throw new Exception("User not found");
            }
        }
    }
}
