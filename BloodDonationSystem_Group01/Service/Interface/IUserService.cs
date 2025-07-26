using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUserService
    {
        public User Login(string email, string password);
        public void Register(string username, string email, string password);
        public void RegisterStaff(string username, string email, string password, User adminUser);
        public List<User> GetAllUsers();
        public User? GetUserById(string userId);
        public User? GetUserByUsername(string username);
        public void UpdateUser(User user);
        public void DeleteUser(string userId);
        public void AddUser(User user);
    }
}
