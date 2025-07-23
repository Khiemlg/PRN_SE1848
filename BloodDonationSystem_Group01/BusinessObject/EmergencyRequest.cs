using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class EmergencyRequest
{
    public string EmergencyId { get; set; } = null!;

    public string RequesterUserId { get; set; } = null!;

    public int BloodTypeId { get; set; }

    public int ComponentId { get; set; }

    public int QuantityNeededMl { get; set; }

    public string? Priority { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? FulfillmentDate { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public virtual BloodType BloodType { get; set; } = null!;

    public virtual BloodComponent Component { get; set; } = null!;

    public virtual ICollection<DonationHistory> DonationHistories { get; set; } = new List<DonationHistory>();

    public virtual ICollection<EmergencyNotification> EmergencyNotifications { get; set; } = new List<EmergencyNotification>();

    public virtual User RequesterUser { get; set; } = null!;
}
