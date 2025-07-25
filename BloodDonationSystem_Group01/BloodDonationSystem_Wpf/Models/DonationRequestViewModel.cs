using System;

namespace BloodDonationSystem_Wpf.Models
{
    public class DonationRequestViewModel
    {
        public string RequestId { get; set; } = string.Empty;
        public string DonorUserId { get; set; } = string.Empty;
        public string DonorUsername { get; set; } = string.Empty;
        public string DonorEmail { get; set; } = string.Empty;
        public int BloodTypeId { get; set; }
        public string BloodTypeName { get; set; } = string.Empty;
        public int ComponentId { get; set; }
        public string ComponentName { get; set; } = string.Empty;
        public DateOnly? PreferredDate { get; set; }
        public string? PreferredTimeSlot { get; set; }
        public string? Status { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? StaffNotes { get; set; }
    }
}
