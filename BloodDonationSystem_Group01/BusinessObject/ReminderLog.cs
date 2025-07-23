using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class ReminderLog
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string ReminderType { get; set; } = null!;

    public DateTime SentAt { get; set; }

    public string Via { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
