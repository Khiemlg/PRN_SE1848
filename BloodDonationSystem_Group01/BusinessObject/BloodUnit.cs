﻿using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class BloodUnit
{
    public string UnitId { get; set; } = null!;

    public string? DonationId { get; set; }

    public int BloodTypeId { get; set; }

    public int ComponentId { get; set; }

    public int VolumeMl { get; set; }

    public DateOnly CollectionDate { get; set; }

    public DateOnly ExpirationDate { get; set; }

    public string? StorageLocation { get; set; }

    public string? TestResults { get; set; }

    public string? Status { get; set; }

    public string? DiscardReason { get; set; }

    public virtual DonationHistory? Donation { get; set; }
}
