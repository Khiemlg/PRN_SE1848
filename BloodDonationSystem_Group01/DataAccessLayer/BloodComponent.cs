using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class BloodComponent
{
    public int ComponentId { get; set; }

    public string ComponentName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<EmergencyRequest> EmergencyRequests { get; set; } = new List<EmergencyRequest>();
}
