using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IDonationHistoryRepository
    {
        public void CreateDonationHistory(DonationHistory donationHistory);
        public List<DonationHistory> GetDonationHistoryByUserId(string userId);
        public List<DonationHistory> GetDonationHistoryByUserIdWithIncludes(string userId);
        public List<DonationHistory> GetAllDonationHistory();
        public DonationHistory? GetDonationHistoryById(string donationId);
        public void UpdateDonationHistory(DonationHistory donationHistory);
    }
}
