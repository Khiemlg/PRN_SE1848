using System;
using System.Windows;
using System.Windows.Controls;
using Service.Interface;
using Service.Implement;

namespace BloodDonationSystem_Wpf
{
    public partial class DiscardBloodUnitWindow : Window
    {
        private readonly IBloodUnitService _bloodUnitService;
        private readonly string _unitId;

        public DiscardBloodUnitWindow(string unitId)
        {
            InitializeComponent();
            _bloodUnitService = new BloodUnitService();
            _unitId = unitId;
            
            InitializeForm();
        }

        private void InitializeForm()
        {
            lblUnitInfo.Content = $"Unit ID: {_unitId}";
            cmbDiscardReason.SelectedIndex = 0; // Default to "Expired"
        }

        private void btnDiscard_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
                return;

            var result = MessageBox.Show($"Are you sure you want to discard unit {_unitId}? This action cannot be undone.", 
                                       "Confirm Discard", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var reason = ((ComboBoxItem)cmbDiscardReason.SelectedItem).Content.ToString();
                    if (!string.IsNullOrWhiteSpace(txtAdditionalNotes.Text))
                    {
                        reason += $" - {txtAdditionalNotes.Text.Trim()}";
                    }

                    _bloodUnitService.DiscardUnit(_unitId, reason);
                    MessageBox.Show("Blood unit discarded successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                    this.DialogResult = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error discarding blood unit: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool ValidateInput()
        {
            if (cmbDiscardReason.SelectedItem == null)
            {
                MessageBox.Show("Please select a reason for discarding.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbDiscardReason.Focus();
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
