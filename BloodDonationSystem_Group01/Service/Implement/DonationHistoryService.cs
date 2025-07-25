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
    public class DonationHistoryService : IDonationHistoryService
    {
        IDonationHistoryRepository donationHistoryRepository;

        public DonationHistoryService()
        {
            donationHistoryRepository = new DonationHistoryRepository();
        }

        public void CreateDonationHistory(DonationHistory donationHistory)
        {
            donationHistoryRepository.CreateDonationHistory(donationHistory);
        }

        public List<DonationHistory> GetDonationHistoryByUserId(string userId)
        {
            return donationHistoryRepository.GetDonationHistoryByUserId(userId);
        }

        public List<DonationHistory> GetAllDonationHistory()
        {
            return donationHistoryRepository.GetAllDonationHistory();
        }

        public DonationHistory? GetDonationHistoryById(string donationId)
        {
            return donationHistoryRepository.GetDonationHistoryById(donationId);
        }

        public void UpdateDonationHistory(DonationHistory donationHistory)
        {
            donationHistoryRepository.UpdateDonationHistory(donationHistory);
        }
    }
}
