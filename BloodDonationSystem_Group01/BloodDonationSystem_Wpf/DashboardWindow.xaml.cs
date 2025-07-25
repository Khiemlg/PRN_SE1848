using BusinessObject;
using System;
using System.Windows;

namespace BloodDonationSystem_Wpf
{
    /// <summary>
    /// Interaction logic for DashboardWindow.xaml
    /// </summary>
    public partial class DashboardWindow : Window
    {
        private User _currentUser;

        public DashboardWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
            SetupDashboard();
        }

        private void SetupDashboard()
        {
            // Set user info
            lblUserInfo.Content = $"Logged in as: {_currentUser.Username} ({GetRoleName(_currentUser.RoleId)})";
            
            // Show appropriate panel based on role
            switch (_currentUser.RoleId)
            {
                case 1: // Admin
                    pnlAdmin.Visibility = Visibility.Visible;
                    lblDashboardTitle.Content = "Administrator Dashboard";
                    break;
                case 2: // Staff
                    pnlStaff.Visibility = Visibility.Visible;
                    lblDashboardTitle.Content = "Staff Dashboard";
                    break;
                case 3: // Member
                    pnlMember.Visibility = Visibility.Visible;
                    lblDashboardTitle.Content = "Member Dashboard";
                    break;
                default:
                    MessageBox.Show("Unknown role. Please contact administrator.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }

        private string GetRoleName(int roleId)
        {
            switch (roleId)
            {
                case 1: return "Administrator";
                case 2: return "Staff";
                case 3: return "Member";
                default: return "Unknown";
            }
        }

        #region Member Functions
        private void btnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditProfileWindow editProfileWindow = new EditProfileWindow(_currentUser);
                editProfileWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Edit Profile: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRegisterDonation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegisterDonationWindow registerDonationWindow = new RegisterDonationWindow(_currentUser);
                registerDonationWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Register Donation: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnViewHistory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewDonationHistoryWindow historyWindow = new ViewDonationHistoryWindow(_currentUser.UserId);
                historyWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Donation History: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Staff Functions
        private void btnManageDonations_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ManageDonationsWindow manageDonationsWindow = new ManageDonationsWindow(_currentUser);
                manageDonationsWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Manage Donations: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnManageBloodUnits_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                ManageBloodUnitsWindow manageBloodUnitsWindow = new ManageBloodUnitsWindow();
                manageBloodUnitsWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Manage Blood Units: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            MessageBox.Show("Manage Blood Units functionality will be implemented soon.", "Coming Soon", MessageBoxButton.OK, MessageBoxImage.Information);
            // TODO: Open ManageBloodUnitsWindow

        }
        #endregion

        #region Admin Functions
        private void btnManageUsers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Manage User Accounts functionality will be implemented soon.", "Coming Soon", MessageBoxButton.OK, MessageBoxImage.Information);
            // TODO: Open ManageUsersWindow
        }

        private void btnRegisterStaff_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegisterStaffWindow registerStaffWindow = new RegisterStaffWindow(_currentUser);
                registerStaffWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Register Staff: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnManageRoles_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Manage User Roles functionality will be implemented soon.", "Coming Soon", MessageBoxButton.OK, MessageBoxImage.Information);
            // TODO: Open ManageRolesWindow
        }

        private void btnSystemReports_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("System Reports functionality will be implemented soon.", "Coming Soon", MessageBoxButton.OK, MessageBoxImage.Information);
            // TODO: Open SystemReportsWindow
        }
        #endregion

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }
    }
}
