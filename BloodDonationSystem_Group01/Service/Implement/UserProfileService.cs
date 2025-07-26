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
    public class UserProfileService : IUserProfileService
    {
        IUserProfileRepository userProfileRepository;

        public UserProfileService()
        {
            userProfileRepository = new UserProfileRepository();
        }

        public UserProfile? GetUserProfileByUserId(string userId)
        {
            return userProfileRepository.GetUserProfileByUserId(userId);
        }

        public void CreateUserProfile(UserProfile userProfile)
        {
            var existingProfile = userProfileRepository.GetUserProfileByUserId(userProfile.UserId);
            if (existingProfile != null)
            {
                throw new Exception("User profile already exists");
            }
            userProfileRepository.CreateUserProfile(userProfile);
        }

        public void UpdateUserProfile(UserProfile userProfile)
        {
            var existingProfile = userProfileRepository.GetUserProfileByUserId(userProfile.UserId);
            if (existingProfile == null)
            {
                throw new Exception("User profile not found");
            }
            userProfileRepository.UpdateUserProfile(userProfile);
        }

        public void DeleteUserProfile(string profileId)
        {
            userProfileRepository.DeleteUserProfile(profileId);
        }

        public List<BloodType> GetAllBloodTypes()
        {
            return userProfileRepository.GetAllBloodTypes();
        }
    }
}
