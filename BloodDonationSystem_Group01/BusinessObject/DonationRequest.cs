using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class DonationRequest
{
    public string RequestId { get; set; } = null!;

    public string DonorUserId { get; set; } = null!;

    public int BloodTypeId { get; set; }

    public int ComponentId { get; set; }

    public DateOnly? PreferredDate { get; set; }

    public string? PreferredTimeSlot { get; set; }

    public string? Status { get; set; }

    public DateTime? RequestDate { get; set; }

    public string? StaffNotes { get; set; }

    public virtual BloodType BloodType { get; set; } = null!;

    public virtual ICollection<DonationHistory> DonationHistories { get; set; } = new List<DonationHistory>();

    public virtual User DonorUser { get; set; } = null!;
}
