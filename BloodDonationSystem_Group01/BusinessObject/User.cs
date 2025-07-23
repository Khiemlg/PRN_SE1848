using DataAccessLayer;
using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Email { get; set; }

    public int RoleId { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<DonationHistory> DonationHistoryDonorUsers { get; set; } = new List<DonationHistory>();

    public virtual ICollection<DonationHistory> DonationHistoryStaffUsers { get; set; } = new List<DonationHistory>();

    public virtual ICollection<DonationRequest> DonationRequests { get; set; } = new List<DonationRequest>();

    public virtual ICollection<EmergencyNotification> EmergencyNotifications { get; set; } = new List<EmergencyNotification>();

    public virtual ICollection<EmergencyRequest> EmergencyRequests { get; set; } = new List<EmergencyRequest>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<ReminderLog> ReminderLogs { get; set; } = new List<ReminderLog>();

    public virtual Role Role { get; set; } = null!;

    public virtual UserProfile? UserProfile { get; set; }
}
