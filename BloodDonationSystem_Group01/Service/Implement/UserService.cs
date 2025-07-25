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

        public void Register(string username, string email, string password)
        {
            // Check if username already exists
            var existingUserByUsername = userRepository.GetUserByUsername(username);
            if (existingUserByUsername != null)
            {
                throw new Exception("Username already exists");
            }

            // Check if email already exists (assuming you have a method to get user by email)
            var allUsers = userRepository.GetAllUsers();
            var existingUserByEmail = allUsers.FirstOrDefault(u => u.Email?.ToLower() == email.ToLower());
            if (existingUserByEmail != null)
            {
                throw new Exception("Email already exists");
            }

            // Create new user
            User newUser = new User
            {
                UserId = Guid.NewGuid().ToString(),
                Username = username.Trim(),
                Email = email.Trim(),
                PasswordHash = password, // In production, this should be hashed
                RoleId = 3, // Default role: Member
                RegistrationDate = DateTime.Now,
                IsActive = true
            };

            userRepository.AddUser(newUser);
        }

        public void RegisterStaff(string username, string email, string password, User adminUser)
        {
            // Check if the current user is Admin
            if (adminUser.RoleId != 1)
            {
                throw new Exception("Only administrators can create staff accounts");
            }

            // Check if username already exists
            var existingUserByUsername = userRepository.GetUserByUsername(username);
            if (existingUserByUsername != null)
            {
                throw new Exception("Username already exists");
            }

            // Check if email already exists
            var allUsers = userRepository.GetAllUsers();
            var existingUserByEmail = allUsers.FirstOrDefault(u => u.Email?.ToLower() == email.ToLower());
            if (existingUserByEmail != null)
            {
                throw new Exception("Email already exists");
            }

            // Create new staff user
            User newStaffUser = new User
            {
                UserId = Guid.NewGuid().ToString(),
                Username = username.Trim(),
                Email = email.Trim(),
                PasswordHash = password, // In production, this should be hashed
                RoleId = 2, // Staff role
                RegistrationDate = DateTime.Now,
                IsActive = true
            };

            userRepository.AddUser(newStaffUser);
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
