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
    public class DonationRequestService : IDonationRequestService
    {
        IDonationRequestRepository donationRequestRepository;

        public DonationRequestService()
        {
            donationRequestRepository = new DonationRequestRepository();
        }

        public void CreateDonationRequest(DonationRequest donationRequest)
        {
            donationRequestRepository.CreateDonationRequest(donationRequest);
        }

        public List<DonationRequest> GetDonationRequestsByUserId(string userId)
        {
            return donationRequestRepository.GetDonationRequestsByUserId(userId);
        }

        public List<BloodComponent> GetAllBloodComponents()
        {
            return donationRequestRepository.GetAllBloodComponents();
        }

        public DonationRequest? GetDonationRequestById(string requestId)
        {
            return donationRequestRepository.GetDonationRequestById(requestId);
        }

        public void UpdateDonationRequest(DonationRequest donationRequest)
        {
            donationRequestRepository.UpdateDonationRequest(donationRequest);
        }
    }
}
