using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUserProfileService
    {
        public UserProfile? GetUserProfileByUserId(string userId);
        public void CreateUserProfile(UserProfile userProfile);
        public void UpdateUserProfile(UserProfile userProfile);
        public void DeleteUserProfile(string profileId);
        public List<BloodType> GetAllBloodTypes();
    }
}
