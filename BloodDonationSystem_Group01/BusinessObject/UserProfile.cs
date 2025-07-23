using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class UserProfile
{
    public string ProfileId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Address { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public int? BloodTypeId { get; set; }

    public string? RhFactor { get; set; }

    public string? MedicalHistory { get; set; }

    public DateOnly? LastBloodDonationDate { get; set; }

    public string? Cccd { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual BloodType? BloodType { get; set; }

    public virtual User User { get; set; } = null!;
}
