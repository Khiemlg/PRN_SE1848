using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IDonationRequestRepository
    {
        public void CreateDonationRequest(DonationRequest donationRequest);
        public List<DonationRequest> GetDonationRequestsByUserId(string userId);
        public List<BloodComponent> GetAllBloodComponents();
        public DonationRequest? GetDonationRequestById(string requestId);
        public void UpdateDonationRequest(DonationRequest donationRequest);
    }
}
