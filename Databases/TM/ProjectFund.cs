using System;
using System.Collections.Generic;

namespace TaskMonitor.Databases.TM;

public partial class ProjectFund
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string ProjectUuid { get; set; } = null!;

    public string? UserUuid { get; set; }

    public double Budget { get; set; }

    public double? ReverseBudget { get; set; }

    public DateTime? RealRelease { get; set; }

    public DateTime? Updated { get; set; }

    public DateTime Created { get; set; }

    /// <summary>
    /// 0 - đang khóa, 1 - hoạt động
    /// </summary>
    public sbyte Status { get; set; }

    /// <summary>
    /// Trạng thái duyệt: 0 - chưa duyệt (đã báo cáo), 1 - đã duyệt, 2 - Bị từ chối
    /// </summary>
    public sbyte? Approved { get; set; }

    public int? Year { get; set; }

    public int? Month { get; set; }

    public string? Note { get; set; }

    public string? Feedback { get; set; }

    public virtual ICollection<OverviewReport> OverviewReport { get; set; } = new List<OverviewReport>();

    public virtual Project ProjectUu { get; set; } = null!;

    public virtual User? UserUu { get; set; }
}
