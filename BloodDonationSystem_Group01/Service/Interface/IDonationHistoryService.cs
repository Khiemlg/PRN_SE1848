using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IDonationHistoryService
    {
        public void CreateDonationHistory(DonationHistory donationHistory);
        public List<DonationHistory> GetDonationHistoryByUserId(string userId);
        public Task<List<DonationHistory>> GetUserDonationHistoryAsync(string userId);
        public List<DonationHistory> GetAllDonationHistory();
        public DonationHistory? GetDonationHistoryById(string donationId);
        public void UpdateDonationHistory(DonationHistory donationHistory);
    }
}
