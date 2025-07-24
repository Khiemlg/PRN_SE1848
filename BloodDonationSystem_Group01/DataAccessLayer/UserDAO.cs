using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserDAO
    {
        BloodDsystemContext _context;
        public UserDAO()
        {
            _context = new BloodDsystemContext();
        }
        public User Login(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);
        }
        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users
                           .Include(u => u.Role)
                           .Include(u => u.UserProfile)
                           .ToList();
        }

        public User? GetUserById(string userId)
        {
            return _context.Users
                           .Include(u => u.Role)
                           .Include(u => u.UserProfile)
                           .FirstOrDefault(u => u.UserId == userId);
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users
                           .Include(u => u.Role)
                           .Include(u => u.UserProfile)
                           .FirstOrDefault(u => u.Username == username);
        }

        public void UpdateUser(User user)
        {
            var existingUser = _context.Users.Find(user.UserId);
            if (existingUser != null)
            {
                _context.Entry(existingUser).CurrentValues.SetValues(user);
                _context.SaveChanges();
            }
        }

        public void DeleteUser(string userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
