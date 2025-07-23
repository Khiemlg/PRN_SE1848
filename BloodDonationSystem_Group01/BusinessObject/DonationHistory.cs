using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class DonationHistory
{
    public string DonationId { get; set; } = null!;

    public string DonorUserId { get; set; } = null!;

    public DateTime DonationDate { get; set; }

    public int BloodTypeId { get; set; }

    public int ComponentId { get; set; }

    public int? QuantityMl { get; set; }

    public string? EligibilityStatus { get; set; }

    public string? ReasonIneligible { get; set; }

    public string? TestingResults { get; set; }

    public string? StaffUserId { get; set; }

    public string? Status { get; set; }

    public string? EmergencyId { get; set; }

    public string? Descriptions { get; set; }

    public string? DonationRequestId { get; set; }

    public virtual BloodType BloodType { get; set; } = null!;

    public virtual ICollection<BloodUnit> BloodUnits { get; set; } = new List<BloodUnit>();

    public virtual DonationRequest? DonationRequest { get; set; }

    public virtual User DonorUser { get; set; } = null!;

    public virtual EmergencyRequest? Emergency { get; set; }

    public virtual User? StaffUser { get; set; }
}
