using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserDAO
    {
        BloodDsystemContext context = new BloodDsystemContext();
        public User Login(string email, string password)
        {
            return context.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);
        }
    }
}
