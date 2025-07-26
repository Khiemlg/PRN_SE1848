using BloodDonationSystem_Wpf.Models;
using BusinessObject;
using Service.Implement;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BloodDonationSystem_Wpf
{
    /// <summary>
    /// Interaction logic for ManageDonationsWindow.xaml
    /// </summary>
    public partial class ManageDonationsWindow : Window
    {
        private User _staffUser;
        private IDonationRequestService _donationRequestService;
        private IUserService _userService;
        private IUserProfileService _userProfileService;
        private List<DonationRequestViewModel> _donationRequests;

        public ManageDonationsWindow(User staffUser)
        {
            InitializeComponent();
            _staffUser = staffUser;
            _donationRequestService = new DonationRequestService();
            _userService = new UserService();
            _userProfileService = new UserProfileService();
            _donationRequests = new List<DonationRequestViewModel>();
            LoadDonationRequests();
        }

        private void LoadDonationRequests()
        {
            try
            {
                var requests = _donationRequestService.GetAllDonationRequests();
                var bloodTypes = _userProfileService.GetAllBloodTypes();
                var components = _donationRequestService.GetAllBloodComponents();
                var users = _userService.GetAllUsers();

                _donationRequests = requests.Select(r => new DonationRequestViewModel
                {
                    RequestId = r.RequestId,
                    DonorUserId = r.DonorUserId,
                    DonorUsername = users.FirstOrDefault(u => u.UserId == r.DonorUserId)?.Username ?? "Unknown",
                    DonorEmail = users.FirstOrDefault(u => u.UserId == r.DonorUserId)?.Email ?? "Unknown",
                    BloodTypeId = r.BloodTypeId,
                    BloodTypeName = bloodTypes.FirstOrDefault(bt => bt.BloodTypeId == r.BloodTypeId)?.TypeName ?? "Unknown",
                    ComponentId = r.ComponentId,
                    ComponentName = components.FirstOrDefault(c => c.ComponentId == r.ComponentId)?.ComponentName ?? "Unknown",
                    PreferredDate = r.PreferredDate,
                    PreferredTimeSlot = r.PreferredTimeSlot,
                    Status = r.Status,
                    RequestDate = r.RequestDate,
                    StaffNotes = r.StaffNotes
                }).ToList();

                dgDonationRequests.ItemsSource = _donationRequests;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading donation requests: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = dgDonationRequests.SelectedItem as DonationRequestViewModel;
            if (selectedRequest == null)
            {
                MessageBox.Show("Please select a donation request to approve.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (selectedRequest.Status != "Pending")
            {
                MessageBox.Show("Only pending requests can be approved.", "Invalid Status", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var request = _donationRequestService.GetDonationRequestById(selectedRequest.RequestId);
                if (request != null)
                {
                    request.Status = "Approved";
                    _donationRequestService.UpdateDonationRequest(request);
                    MessageBox.Show("Donation request approved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadDonationRequests();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error approving request: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnReject_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = dgDonationRequests.SelectedItem as DonationRequestViewModel;
            if (selectedRequest == null)
            {
                MessageBox.Show("Please select a donation request to reject.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (selectedRequest.Status != "Pending")
            {
                MessageBox.Show("Only pending requests can be rejected.", "Invalid Status", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to reject this donation request?", "Confirm Rejection", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var request = _donationRequestService.GetDonationRequestById(selectedRequest.RequestId);
                    if (request != null)
                    {
                        request.Status = "Rejected";
                        _donationRequestService.UpdateDonationRequest(request);
                        MessageBox.Show("Donation request rejected.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadDonationRequests();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error rejecting request: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnProcessDonation_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = dgDonationRequests.SelectedItem as DonationRequestViewModel;
            if (selectedRequest == null)
            {
                MessageBox.Show("Please select a donation request to process.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (selectedRequest.Status != "Approved")
            {
                MessageBox.Show("Only approved requests can be processed.", "Invalid Status", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var request = _donationRequestService.GetDonationRequestById(selectedRequest.RequestId);
                if (request != null)
                {
                    ProcessDonationWindow processWindow = new ProcessDonationWindow(request, _staffUser);
                    processWindow.ShowDialog();
                    LoadDonationRequests(); // Refresh after processing
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening process window: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
