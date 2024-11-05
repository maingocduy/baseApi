using System;
using System.Collections.Generic;

namespace TaskMonitor.Databases.TM;

public partial class Sessions
{
    public long Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string AccountUuid { get; set; } = null!;

    public string? Ip { get; set; }

    public DateTime TimeLogin { get; set; }

    public DateTime? TimeLogout { get; set; }

    /// <summary>
    /// 0: LogIn - 1: LogOut
    /// </summary>
    public sbyte Status { get; set; }

    public virtual Account AccountUu { get; set; } = null!;
}
