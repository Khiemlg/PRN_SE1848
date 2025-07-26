using BusinessObject;
using Service.Implement;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BloodDonationSystem_Wpf
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        IUserService UserService = new UserService();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Password))
                {
                    MessageBox.Show("Please enter both email and password.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                User user = UserService.Login(txtEmail.Text, txtPassword.Password);
                if (user != null)
                {
                    MessageBox.Show($"Welcome back, {user.Username}!", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                    // Open Dashboard with user information
                    DashboardWindow dashboard = new DashboardWindow(user);
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid email or password. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}
