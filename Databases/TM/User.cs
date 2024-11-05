using System;
using System.Collections.Generic;

namespace TaskMonitor.Databases.TM;

public partial class User
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public string? UserName { get; set; }

    public string? Code { get; set; }

    /// <summary>
    /// 0-Nam , 1-Nữ , 2 - khác
    /// </summary>
    public sbyte Gender { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Matp { get; set; }

    public string? Maqh { get; set; }

    public string? Xaid { get; set; }

    public string? Address { get; set; }

    public string? Note { get; set; }

    public DateOnly? Birthday { get; set; }

    public DateTime? Updated { get; set; }

    public DateTime Created { get; set; }

    /// <summary>
    /// 0 - đang khóa, 1 - hoạt động
    /// </summary>
    public sbyte Status { get; set; }

    public virtual ICollection<Account> Account { get; set; } = new List<Account>();

    public virtual DevvnQuanhuyen? MaqhNavigation { get; set; }

    public virtual DevvnTinhthanhpho? MatpNavigation { get; set; }

    public virtual ICollection<OverviewReport> OverviewReport { get; set; } = new List<OverviewReport>();

    public virtual ICollection<ProjectFund> ProjectFund { get; set; } = new List<ProjectFund>();

    public virtual ICollection<UserProjects> UserProjects { get; set; } = new List<UserProjects>();

    public virtual DevvnXaphuongthitran? Xa { get; set; }
}
