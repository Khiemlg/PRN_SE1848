using BusinessObject;
using Service.Implement;
using Service.Interface;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BloodDonationSystem_Wpf
{
    /// <summary>
    /// Interaction logic for RegisterDonationWindow.xaml
    /// </summary>
    public partial class RegisterDonationWindow : Window
    {
        private User _currentUser;
        private IDonationRequestService _donationRequestService;
        private IUserProfileService _userProfileService;

        public RegisterDonationWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _donationRequestService = new DonationRequestService();
            _userProfileService = new UserProfileService();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Load blood types
                var bloodTypes = _userProfileService.GetAllBloodTypes();
                cmbBloodType.ItemsSource = bloodTypes;

                // Load blood components
                var components = _donationRequestService.GetAllBloodComponents();
                cmbComponentType.ItemsSource = components;

                // Set minimum date to today
                dpPreferredDate.DisplayDateStart = DateTime.Today;
                dpPreferredDate.SelectedDate = DateTime.Today.AddDays(1); // Default to tomorrow

                // Set default time slot
                cmbTimeSlot.SelectedIndex = 0;

                // Pre-select user's blood type if available
                var userProfile = _userProfileService.GetUserProfileByUserId(_currentUser.UserId);
                if (userProfile?.BloodTypeId != null)
                {
                    cmbBloodType.SelectedValue = userProfile.BloodTypeId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input
                if (cmbBloodType.SelectedValue == null)
                {
                    MessageBox.Show("Please select a blood type.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (cmbComponentType.SelectedValue == null)
                {
                    MessageBox.Show("Please select a component type.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!dpPreferredDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select a preferred date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (dpPreferredDate.SelectedDate < DateTime.Today)
                {
                    MessageBox.Show("Preferred date cannot be in the past.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (cmbTimeSlot.SelectedItem == null)
                {
                    MessageBox.Show("Please select a time slot.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Create donation request
                var donationRequest = new DonationRequest
                {
                    RequestId = Guid.NewGuid().ToString(),
                    DonorUserId = _currentUser.UserId,
                    BloodTypeId = (int)cmbBloodType.SelectedValue,
                    ComponentId = (int)cmbComponentType.SelectedValue,
                    PreferredDate = DateOnly.FromDateTime(dpPreferredDate.SelectedDate.Value),
                    PreferredTimeSlot = (cmbTimeSlot.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    Status = "Pending",
                    RequestDate = DateTime.Now,
                    StaffNotes = txtNotes.Text.Trim()
                };

                _donationRequestService.CreateDonationRequest(donationRequest);

                MessageBox.Show("Donation request submitted successfully!\nYou will be contacted by our staff for confirmation.", 
                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting donation request: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
