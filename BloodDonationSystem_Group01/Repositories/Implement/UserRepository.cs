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
        public User Login(string email, string password)
        {
            return UserDAO.Login(email, password);
        }
    }
}
