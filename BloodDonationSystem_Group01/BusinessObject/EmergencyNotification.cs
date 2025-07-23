using DataAccessLayer;
using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class EmergencyNotification
{
    public string NotificationId { get; set; } = null!;

    public string EmergencyId { get; set; } = null!;

    public string RecipientUserId { get; set; } = null!;

    public DateTime? SentDate { get; set; }

    public string DeliveryMethod { get; set; } = null!;

    public bool? IsRead { get; set; }

    public string? ResponseStatus { get; set; }

    public string? Message { get; set; }

    public virtual EmergencyRequest Emergency { get; set; } = null!;

    public virtual User RecipientUser { get; set; } = null!;
}
