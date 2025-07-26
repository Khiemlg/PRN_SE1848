using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Service.Interface;
using Service.Implement;
using BusinessObject;

namespace BloodDonationSystem_Wpf
{
    public partial class AddEditBloodUnitWindow : Window
    {
        private readonly IBloodUnitService _bloodUnitService;

        public AddEditBloodUnitWindow()
        {
            InitializeComponent();
            _bloodUnitService = new BloodUnitService();
            
            InitializeForm();
        }

        private void InitializeForm()
        {
            lblTitle.Content = "Add Blood Unit";
            
            // Generate unique Unit ID
            GenerateUnitId();
            
            // Set default values
            dpCollectionDate.SelectedDate = DateTime.Now;
            dpExpirationDate.SelectedDate = DateTime.Now.AddDays(42); // Blood typically expires in 42 days
            cmbTestResults.SelectedIndex = 0; // Pending
        }

        private void GenerateUnitId()
        {
            // Generate unique Unit ID format: UNIT_YYYYMMDD_HHMMSS
            var now = DateTime.Now;
            var unitId = $"UNIT_{now:yyyyMMdd}_{now:HHmmss}";
            txtUnitId.Text = unitId;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                var bloodUnit = new BloodUnit
                {
                    UnitId = txtUnitId.Text.Trim(),
                    BloodTypeId = int.Parse(((ComboBoxItem)cmbBloodType.SelectedItem).Tag.ToString()!),
                    ComponentId = int.Parse(((ComboBoxItem)cmbComponent.SelectedItem).Tag.ToString()!),
                    VolumeMl = int.Parse(txtVolume.Text.Trim()),
                    CollectionDate = DateOnly.FromDateTime(dpCollectionDate.SelectedDate!.Value),
                    ExpirationDate = DateOnly.FromDateTime(dpExpirationDate.SelectedDate!.Value),
                    StorageLocation = txtStorageLocation.Text.Trim(),
                    TestResults = ((ComboBoxItem)cmbTestResults.SelectedItem).Content.ToString(),
                    Status = "Available" // New units are available by default
                };

                _bloodUnitService.AddBloodUnit(bloodUnit);
                MessageBox.Show("Blood unit added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving blood unit: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtUnitId.Text))
            {
                MessageBox.Show("Please enter a Unit ID.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtUnitId.Focus();
                return false;
            }

            if (cmbBloodType.SelectedItem == null)
            {
                MessageBox.Show("Please select a blood type.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbBloodType.Focus();
                return false;
            }

            if (cmbComponent.SelectedItem == null)
            {
                MessageBox.Show("Please select a component.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbComponent.Focus();
                return false;
            }

            if (!int.TryParse(txtVolume.Text.Trim(), out int volume) || volume <= 0)
            {
                MessageBox.Show("Please enter a valid volume (positive number).", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtVolume.Focus();
                return false;
            }

            if (!dpCollectionDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select a collection date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                dpCollectionDate.Focus();
                return false;
            }

            if (!dpExpirationDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select an expiration date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                dpExpirationDate.Focus();
                return false;
            }

            if (dpExpirationDate.SelectedDate <= dpCollectionDate.SelectedDate)
            {
                MessageBox.Show("Expiration date must be after collection date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                dpExpirationDate.Focus();
                return false;
            }

            if (cmbTestResults.SelectedItem == null)
            {
                MessageBox.Show("Please select test results.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbTestResults.Focus();
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
