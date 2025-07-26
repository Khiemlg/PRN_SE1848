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
    public partial class ManageBloodUnitsWindow : Window
    {
        private readonly IBloodUnitService _bloodUnitService;
        private List<BloodUnitViewModel> _allBloodUnits;

        public ManageBloodUnitsWindow()
        {
            InitializeComponent();
            _bloodUnitService = new BloodUnitService();
            _allBloodUnits = new List<BloodUnitViewModel>();
            LoadBloodUnits();
        }

        private async void LoadBloodUnits()
        {
            try
            {
                var bloodUnits = await _bloodUnitService.GetAllBloodUnitsAsync();
                _allBloodUnits = bloodUnits.Select(unit => new BloodUnitViewModel
                {
                    UnitId = unit.UnitId,
                    BloodTypeName = GetBloodTypeName(unit.BloodTypeId),
                    ComponentName = GetComponentName(unit.ComponentId),
                    VolumeMl = unit.VolumeMl,
                    CollectionDate = unit.CollectionDate.ToDateTime(TimeOnly.MinValue),
                    ExpirationDate = unit.ExpirationDate.ToDateTime(TimeOnly.MinValue),
                    StorageLocation = unit.StorageLocation ?? "Unknown",
                    Status = unit.Status ?? "Unknown",
                    TestResults = unit.TestResults ?? "Pending",
                    DiscardReason = unit.DiscardReason ?? "",
                    IsExpired = unit.ExpirationDate < DateOnly.FromDateTime(DateTime.Now),
                    IsExpiringSoon = unit.ExpirationDate <= DateOnly.FromDateTime(DateTime.Now.AddDays(7)) &&
                                   unit.ExpirationDate >= DateOnly.FromDateTime(DateTime.Now)
                }).OrderByDescending(u => u.CollectionDate).ToList();

                dgBloodUnits.ItemsSource = _allBloodUnits;
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading blood units: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetBloodTypeName(int bloodTypeId)
        {
            return bloodTypeId switch
            {
                1 => "A+",
                2 => "A-",
                3 => "B+",
                4 => "B-",
                5 => "AB+",
                6 => "AB-",
                7 => "O+",
                8 => "O-",
                _ => "Unknown"
            };
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
            txtTotalUnits.Text = _allBloodUnits.Count.ToString();
            txtAvailableUnits.Text = _allBloodUnits.Count(u => u.Status == "Available").ToString();
            txtExpiredUnits.Text = _allBloodUnits.Count(u => u.IsExpired).ToString();
            txtUsedUnits.Text = _allBloodUnits.Count(u => u.Status == "Used").ToString();
        }

        private void cmbStatusFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void cmbBloodTypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void cmbComponentFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void chkShowExpiringSoon_Checked(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void chkShowExpiringSoon_Unchecked(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            if (_allBloodUnits == null) return;

            var filtered = _allBloodUnits.AsEnumerable();

            // Status filter
            var selectedStatus = (cmbStatusFilter.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (!string.IsNullOrEmpty(selectedStatus) && selectedStatus != "All")
            {
                filtered = filtered.Where(u => u.Status == selectedStatus);
            }

            // Blood type filter
            var selectedBloodType = (cmbBloodTypeFilter.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (!string.IsNullOrEmpty(selectedBloodType) && selectedBloodType != "All")
            {
                filtered = filtered.Where(u => u.BloodTypeName == selectedBloodType);
            }

            // Component filter
            var selectedComponent = (cmbComponentFilter.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (!string.IsNullOrEmpty(selectedComponent) && selectedComponent != "All")
            {
                filtered = filtered.Where(u => u.ComponentName == selectedComponent);
            }

            // Expiring soon filter
            if (chkShowExpiringSoon.IsChecked == true)
            {
                filtered = filtered.Where(u => u.IsExpiringSoon || u.IsExpired);
            }

            dgBloodUnits.ItemsSource = filtered.ToList();
        }

        private void btnAddUnit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddEditBloodUnitWindow addWindow = new AddEditBloodUnitWindow();
                if (addWindow.ShowDialog() == true)
                {
                    LoadBloodUnits(); // Refresh the list
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Add Unit window: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDiscard_Click(object sender, RoutedEventArgs e)
        {
            var selectedUnit = dgBloodUnits.SelectedItem as BloodUnitViewModel;
            if (selectedUnit == null)
            {
                MessageBox.Show("Please select a blood unit to discard.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (selectedUnit.Status == "Used")
            {
                MessageBox.Show("Used units cannot be discarded.", "Invalid Status", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                DiscardBloodUnitWindow discardWindow = new DiscardBloodUnitWindow(selectedUnit.UnitId);
                if (discardWindow.ShowDialog() == true)
                {
                    LoadBloodUnits(); // Refresh the list
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Discard Unit window: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class BloodUnitViewModel
    {
        public string UnitId { get; set; } = string.Empty;
        public string BloodTypeName { get; set; } = string.Empty;
        public string ComponentName { get; set; } = string.Empty;
        public int VolumeMl { get; set; }
        public DateTime CollectionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string StorageLocation { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string TestResults { get; set; } = string.Empty;
        public string DiscardReason { get; set; } = string.Empty;
        public bool IsExpired { get; set; }
        public bool IsExpiringSoon { get; set; }
    }
}
