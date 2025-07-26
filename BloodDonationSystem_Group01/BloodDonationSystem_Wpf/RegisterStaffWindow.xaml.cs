using BusinessObject;
using Service.Implement;
using Service.Interface;
using System;
using System.Windows;

namespace BloodDonationSystem_Wpf
{
    /// <summary>
    /// Interaction logic for RegisterStaffWindow.xaml
    /// </summary>
    public partial class RegisterStaffWindow : Window
    {
        IUserService UserService = new UserService();
        private User _adminUser;

        public RegisterStaffWindow(User adminUser)
        {
            InitializeComponent();
            _adminUser = adminUser;
        }

        private void btnRegisterStaff_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Password) ||
                    string.IsNullOrWhiteSpace(txtConfirmPassword.Password))
                {
                    MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (txtPassword.Password != txtConfirmPassword.Password)
                {
                    MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Check if email is valid format
                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Register staff using service
                UserService.RegisterStaff(txtUsername.Text.Trim(), txtEmail.Text.Trim(), txtPassword.Password, _adminUser);

                MessageBox.Show("Staff account created successfully!\nThe new staff can now login with their credentials.", 
                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Staff registration failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
