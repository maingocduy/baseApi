using System;
using System.Collections.Generic;

namespace TaskMonitor.Databases.TM;

public partial class Account
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string UserUuid { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string RoleUuid { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public DateTime Created { get; set; }

    /// <summary>
    /// 0-không sử dụng , 1- sử dụng , 2-khóa
    /// </summary>
    public sbyte Status { get; set; }

    public virtual ICollection<NotifyAcc> NotifyAcc { get; set; } = new List<NotifyAcc>();

    public virtual ICollection<Otp> Otp { get; set; } = new List<Otp>();

    public virtual Role RoleUu { get; set; } = null!;

    public virtual ICollection<Sessions> Sessions { get; set; } = new List<Sessions>();

    public virtual User UserUu { get; set; } = null!;
}
