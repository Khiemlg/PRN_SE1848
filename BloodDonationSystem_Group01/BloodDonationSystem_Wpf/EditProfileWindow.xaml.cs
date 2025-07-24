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
    /// Interaction logic for EditProfileWindow.xaml
    /// </summary>
    public partial class EditProfileWindow : Window
    {
        private User _currentUser;
        private UserProfile? _userProfile;
        private IUserProfileService _userProfileService;

        public EditProfileWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
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

                // Load user profile
                _userProfile = _userProfileService.GetUserProfileByUserId(_currentUser.UserId);
                
                if (_userProfile != null)
                {
                    // Fill form with existing data
                    txtFullName.Text = _userProfile.FullName ?? "";
                    dpDateOfBirth.SelectedDate = _userProfile.DateOfBirth?.ToDateTime(TimeOnly.MinValue);
                    
                    // Set gender
                    var genderItem = cmbGender.Items.Cast<ComboBoxItem>()
                        .FirstOrDefault(item => item.Content.ToString() == _userProfile.Gender);
                    cmbGender.SelectedItem = genderItem;

                    txtPhoneNumber.Text = _userProfile.PhoneNumber ?? "";
                    txtCccd.Text = _userProfile.Cccd ?? "";
                    txtAddress.Text = _userProfile.Address ?? "";
                    
                    // Set blood type
                    if (_userProfile.BloodTypeId != null)
                    {
                        cmbBloodType.SelectedValue = _userProfile.BloodTypeId;
                    }

                    // Set RH factor
                    var rhItem = cmbRhFactor.Items.Cast<ComboBoxItem>()
                        .FirstOrDefault(item => item.Content.ToString() == _userProfile.RhFactor);
                    cmbRhFactor.SelectedItem = rhItem;

                    txtMedicalHistory.Text = _userProfile.MedicalHistory ?? "";
                    dpLastDonationDate.SelectedDate = _userProfile.LastBloodDonationDate?.ToDateTime(TimeOnly.MinValue);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading profile data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    MessageBox.Show("Full Name is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Create or update user profile
                if (_userProfile == null)
                {
                    _userProfile = new UserProfile
                    {
                        ProfileId = Guid.NewGuid().ToString(),
                        UserId = _currentUser.UserId
                    };
                }

                // Update profile data
                _userProfile.FullName = txtFullName.Text.Trim();
                _userProfile.DateOfBirth = dpDateOfBirth.SelectedDate.HasValue ? DateOnly.FromDateTime(dpDateOfBirth.SelectedDate.Value) : null;
                _userProfile.Gender = (cmbGender.SelectedItem as ComboBoxItem)?.Content.ToString();
                _userProfile.PhoneNumber = txtPhoneNumber.Text.Trim();
                _userProfile.Cccd = txtCccd.Text.Trim();
                _userProfile.Address = txtAddress.Text.Trim();
                _userProfile.BloodTypeId = (int?)cmbBloodType.SelectedValue;
                _userProfile.RhFactor = (cmbRhFactor.SelectedItem as ComboBoxItem)?.Content.ToString();
                _userProfile.MedicalHistory = txtMedicalHistory.Text.Trim();
                _userProfile.LastBloodDonationDate = dpLastDonationDate.SelectedDate.HasValue ? DateOnly.FromDateTime(dpLastDonationDate.SelectedDate.Value) : null;

                // Save to database
                if (_userProfileService.GetUserProfileByUserId(_currentUser.UserId) == null)
                {
                    _userProfileService.CreateUserProfile(_userProfile);
                }
                else
                {
                    _userProfileService.UpdateUserProfile(_userProfile);
                }

                MessageBox.Show("Profile saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving profile: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
