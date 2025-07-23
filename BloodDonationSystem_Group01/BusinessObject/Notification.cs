using DataAccessLayer;
using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Notification
{
    public string NotificationId { get; set; } = null!;

    public string RecipientUserId { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string? Type { get; set; }

    public DateTime? SentDate { get; set; }

    public bool? IsRead { get; set; }

    public virtual User RecipientUser { get; set; } = null!;
}
