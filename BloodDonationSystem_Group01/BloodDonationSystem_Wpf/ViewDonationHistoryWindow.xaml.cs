using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Service.Interface;
using Service.Implement;
using BusinessObject;

namespace BloodDonationSystem_Wpf
{
    public partial class ViewDonationHistoryWindow : Window
    {
        private readonly IDonationHistoryService _donationHistoryService;
        private readonly string _userId;
        private List<DonationHistoryViewModel> _allDonations;

        public ViewDonationHistoryWindow(string userId)
        {
            InitializeComponent();
            _userId = userId;
            _donationHistoryService = new DonationHistoryService();
            _allDonations = new List<DonationHistoryViewModel>();
            LoadDonationHistory();
        }

        private async void LoadDonationHistory()
        {
            try
            {
                var donations = await _donationHistoryService.GetUserDonationHistoryAsync(_userId);
                _allDonations = donations.Select(d => new DonationHistoryViewModel
                {
                    DonationId = d.DonationId,
                    DonationDate = d.DonationDate,
                    BloodTypeName = d.BloodType?.TypeName ?? "Unknown",
                    ComponentName = GetComponentName(d.ComponentId),
                    QuantityMl = d.QuantityMl,
                    Status = d.Status ?? "Unknown",
                    EligibilityStatus = d.EligibilityStatus ?? "Unknown",
                    TestingResults = d.TestingResults ?? "Pending",
                    StaffUserName = d.StaffUser?.Username ?? "System",
                    Descriptions = d.Descriptions ?? ""
                }).OrderByDescending(d => d.DonationDate).ToList();

                dgDonationHistory.ItemsSource = _allDonations;
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading donation history: {ex.Message}", "Error", 
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetComponentName(int componentId)
        {
            return componentId switch
            {
                1 => "Whole Blood",
                2 => "Red Blood Cells",
                3 => "Plasma",
                4 => "Platelets",
                _ => "Unknown"
            };
        }

        private void UpdateStatistics()
        {
            var completedDonations = _allDonations.Where(d => d.Status == "Completed").ToList();
            
            txtTotalDonations.Text = completedDonations.Count.ToString();
            txtTotalVolume.Text = completedDonations.Sum(d => d.QuantityMl ?? 0).ToString();
            
            if (completedDonations.Any())
            {
                var lastDonation = completedDonations.OrderByDescending(d => d.DonationDate).First();
                txtLastDonation.Text = lastDonation.DonationDate.ToString("dd/MM/yyyy");
                
                // Calculate next eligible date (typically 8 weeks after last donation)
                var nextEligibleDate = lastDonation.DonationDate.AddDays(56);
                if (nextEligibleDate <= DateTime.Now)
                {
                    txtNextEligible.Text = "Now";
                    txtNextEligible.Foreground = new System.Windows.Media.SolidColorBrush(
                        System.Windows.Media.Color.FromRgb(56, 142, 60)); // Green
                }
                else
                {
                    txtNextEligible.Text = nextEligibleDate.ToString("dd/MM/yyyy");
                    txtNextEligible.Foreground = new System.Windows.Media.SolidColorBrush(
                        System.Windows.Media.Color.FromRgb(255, 143, 0)); // Orange
                }
            }
            else
            {
                txtLastDonation.Text = "Never";
                txtNextEligible.Text = "Now";
            }
        }

        private void cmbStatusFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            if (_allDonations == null) return;

            var filtered = _allDonations.AsEnumerable();

            // Status filter
            var selectedStatus = (cmbStatusFilter.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (!string.IsNullOrEmpty(selectedStatus) && selectedStatus != "All")
            {
                filtered = filtered.Where(d => d.Status == selectedStatus);
            }

            dgDonationHistory.ItemsSource = filtered.ToList();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class DonationHistoryViewModel
    {
        public string DonationId { get; set; } = string.Empty;
        public DateTime DonationDate { get; set; }
        public string BloodTypeName { get; set; } = string.Empty;
        public string ComponentName { get; set; } = string.Empty;
        public int? QuantityMl { get; set; }
        public string Status { get; set; } = string.Empty;
        public string EligibilityStatus { get; set; } = string.Empty;
        public string TestingResults { get; set; } = string.Empty;
        public string StaffUserName { get; set; } = string.Empty;
        public string Descriptions { get; set; } = string.Empty;
    }
}
