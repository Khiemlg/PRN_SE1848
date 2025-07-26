using BusinessObject;
using Service.Implement;
using Service.Interface;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BloodDonationSystem_Wpf
{
    /// <summary>
    /// Interaction logic for ProcessDonationWindow.xaml
    /// </summary>
    public partial class ProcessDonationWindow : Window
    {
        private DonationRequest _donationRequest;
        private User _staffUser;
        private IDonationRequestService _donationRequestService;
        private IDonationHistoryService _donationHistoryService;
        private IUserService _userService;
        private IUserProfileService _userProfileService;

        public ProcessDonationWindow(DonationRequest donationRequest, User staffUser)
        {
            InitializeComponent();
            _donationRequest = donationRequest;
            _staffUser = staffUser;
            _donationRequestService = new DonationRequestService();
            _donationHistoryService = new DonationHistoryService();
            _userService = new UserService();
            _userProfileService = new UserProfileService();
            LoadDonationRequestInfo();
        }

        private void LoadDonationRequestInfo()
        {
            try
            {
                // Load donor information
                var donor = _userService.GetUserById(_donationRequest.DonorUserId);
                var bloodTypes = _userProfileService.GetAllBloodTypes();
                var components = _donationRequestService.GetAllBloodComponents();

                if (donor != null)
                {
                    txtDonorName.Text = donor.Username;
                }

                var bloodType = bloodTypes.Find(bt => bt.BloodTypeId == _donationRequest.BloodTypeId);
                if (bloodType != null)
                {
                    txtBloodType.Text = bloodType.TypeName;
                }

                var component = components.Find(c => c.ComponentId == _donationRequest.ComponentId);
                if (component != null)
                {
                    txtComponent.Text = component.ComponentName;
                }

                // Set default values
                cmbEligibilityStatus.SelectedIndex = 0; // Eligible
                cmbTestingResults.SelectedIndex = 2; // Pending
                cmbDonationStatus.SelectedIndex = 0; // Completed
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading donation request info: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnComplete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input
                if (cmbEligibilityStatus.SelectedItem == null)
                {
                    MessageBox.Show("Please select eligibility status.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var eligibilityStatus = (cmbEligibilityStatus.SelectedItem as ComboBoxItem)?.Content.ToString();
                
                if (eligibilityStatus == "Not Eligible" && string.IsNullOrWhiteSpace(txtReasonIneligible.Text))
                {
                    MessageBox.Show("Please provide reason for ineligibility.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (eligibilityStatus == "Eligible")
                {
                    if (string.IsNullOrWhiteSpace(txtQuantity.Text) || !int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
                    {
                        MessageBox.Show("Please enter a valid quantity in ML.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                // Create donation history record
                var donationHistory = new DonationHistory
                {
                    DonationId = Guid.NewGuid().ToString(),
                    DonorUserId = _donationRequest.DonorUserId,
                    DonationDate = DateTime.Now,
                    BloodTypeId = _donationRequest.BloodTypeId,
                    ComponentId = _donationRequest.ComponentId,
                    QuantityMl = eligibilityStatus == "Eligible" ? int.Parse(txtQuantity.Text) : null,
                    EligibilityStatus = eligibilityStatus,
                    ReasonIneligible = eligibilityStatus == "Not Eligible" ? txtReasonIneligible.Text.Trim() : null,
                    TestingResults = (cmbTestingResults.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    StaffUserId = _staffUser.UserId,
                    Status = (cmbDonationStatus.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    Descriptions = txtDescription.Text.Trim(),
                    DonationRequestId = _donationRequest.RequestId
                };

                // Save donation history
                _donationHistoryService.CreateDonationHistory(donationHistory);

                // Update donation request status
                _donationRequest.Status = "Completed";
                _donationRequestService.UpdateDonationRequest(_donationRequest);

                // Update user's last donation date if eligible and completed
                if (eligibilityStatus == "Eligible" && donationHistory.Status == "Completed")
                {
                    var userProfile = _userProfileService.GetUserProfileByUserId(_donationRequest.DonorUserId);
                    if (userProfile != null)
                    {
                        userProfile.LastBloodDonationDate = DateOnly.FromDateTime(DateTime.Now);
                        _userProfileService.UpdateUserProfile(userProfile);
                    }
                }

                MessageBox.Show("Donation process completed successfully!\nDonation history has been recorded.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error completing donation process: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
