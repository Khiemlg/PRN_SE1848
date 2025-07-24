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
    public class UserProfileRepository : IUserProfileRepository
    {
        public UserProfile? GetUserProfileByUserId(string userId)
        {
            try
            {
                using var context = new BloodDsystemContext();
                return context.UserProfiles
                    .Where(p => p.UserId == userId)
                    .FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void CreateUserProfile(UserProfile userProfile)
        {
            try
            {
                using var context = new BloodDsystemContext();
                context.UserProfiles.Add(userProfile);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating user profile: {ex.Message}");
            }
        }

        public void UpdateUserProfile(UserProfile userProfile)
        {
            try
            {
                using var context = new BloodDsystemContext();
                context.UserProfiles.Update(userProfile);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user profile: {ex.Message}");
            }
        }

        public void DeleteUserProfile(string profileId)
        {
            try
            {
                using var context = new BloodDsystemContext();
                var profile = context.UserProfiles.Find(profileId);
                if (profile != null)
                {
                    context.UserProfiles.Remove(profile);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting user profile: {ex.Message}");
            }
        }

        public List<BloodType> GetAllBloodTypes()
        {
            try
            {
                using var context = new BloodDsystemContext();
                return context.BloodTypes.ToList();
            }
            catch (Exception)
            {
                return new List<BloodType>();
            }
        }
    }
}
