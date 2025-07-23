using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class OtpCode
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Code { get; set; } = null!;

    public DateTime ExpiredAt { get; set; }

    public bool? IsUsed { get; set; }
}
