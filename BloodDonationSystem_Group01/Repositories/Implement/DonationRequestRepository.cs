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
    public class DonationRequestRepository : IDonationRequestRepository
    {
        public void CreateDonationRequest(DonationRequest donationRequest)
        {
            try
            {
                using var context = new BloodDsystemContext();
                context.DonationRequests.Add(donationRequest);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating donation request: {ex.Message}");
            }
        }

        public List<DonationRequest> GetDonationRequestsByUserId(string userId)
        {
            try
            {
                using var context = new BloodDsystemContext();
                return context.DonationRequests
                    .Where(dr => dr.DonorUserId == userId)
                    .OrderByDescending(dr => dr.RequestDate)
                    .ToList();
            }
            catch (Exception)
            {
                return new List<DonationRequest>();
            }
        }

        public List<BloodComponent> GetAllBloodComponents()
        {
            try
            {
                using var context = new BloodDsystemContext();
                return context.BloodComponents.ToList();
            }
            catch (Exception)
            {
                return new List<BloodComponent>();
            }
        }

        public DonationRequest? GetDonationRequestById(string requestId)
        {
            try
            {
                using var context = new BloodDsystemContext();
                return context.DonationRequests.Find(requestId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void UpdateDonationRequest(DonationRequest donationRequest)
        {
            try
            {
                using var context = new BloodDsystemContext();
                context.DonationRequests.Update(donationRequest);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating donation request: {ex.Message}");
            }
        }
    }
}
