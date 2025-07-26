using BusinessObject;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class DonationHistoryRepository : IDonationHistoryRepository
    {
        public void CreateDonationHistory(DonationHistory donationHistory)
        {
            try
            {
                using var context = new BloodDsystemContext();
                context.DonationHistories.Add(donationHistory);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating donation history: {ex.Message}");
            }
        }

        public List<DonationHistory> GetDonationHistoryByUserId(string userId)
        {
            try
            {
                using var context = new BloodDsystemContext();
                return context.DonationHistories
                    .Where(dh => dh.DonorUserId == userId)
                    .OrderByDescending(dh => dh.DonationDate)
                    .ToList();
            }
            catch (Exception)
            {
                return new List<DonationHistory>();
            }
        }

        public List<DonationHistory> GetDonationHistoryByUserIdWithIncludes(string userId)
        {
            try
            {
                using var context = new BloodDsystemContext();
                return context.DonationHistories
                    .Where(dh => dh.DonorUserId == userId)
                    .Include(dh => dh.BloodType)
                    .Include(dh => dh.StaffUser)
                    .Include(dh => dh.DonationRequest)
                    .OrderByDescending(dh => dh.DonationDate)
                    .ToList();
            }
            catch (Exception)
            {
                return new List<DonationHistory>();
            }
        }

        public List<DonationHistory> GetAllDonationHistory()
        {
            try
            {
                using var context = new BloodDsystemContext();
                return context.DonationHistories
                    .OrderByDescending(dh => dh.DonationDate)
                    .ToList();
            }
            catch (Exception)
            {
                return new List<DonationHistory>();
            }
        }

        public DonationHistory? GetDonationHistoryById(string donationId)
        {
            try
            {
                using var context = new BloodDsystemContext();
                return context.DonationHistories.Find(donationId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void UpdateDonationHistory(DonationHistory donationHistory)
        {
            try
            {
                using var context = new BloodDsystemContext();
                context.DonationHistories.Update(donationHistory);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating donation history: {ex.Message}");
            }
        }
    }
}
