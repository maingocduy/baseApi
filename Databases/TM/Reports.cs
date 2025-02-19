using System;
using System.Collections.Generic;

namespace BaseApi.Databases.TM;

public partial class Reports
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string? Title { get; set; }

    public string UpUuid { get; set; } = null!;

    public string? Note { get; set; }

    public string? RejectedReason { get; set; }

    /// <summary>
    /// 0 -Từ chối , 1 - Đã duyệt , 2 - Lên kế hoạch , 3 - Chờ duyệt , 4 - Đang thực hiện
    /// 
    /// </summary>
    public sbyte? State { get; set; }

    public int? Month { get; set; }

    public int? Year { get; set; }

    /// <summary>
    /// Ngày gửi báo báo
    /// </summary>
    public DateTime? Completed { get; set; }

    public DateTime Created { get; set; }

    /// <summary>
    /// 0-không sử dụng , 1- sử dụng
    /// </summary>
    public sbyte Status { get; set; }

    public virtual ICollection<ActivityReport> ActivityReport { get; set; } = new List<ActivityReport>();

    public virtual ICollection<OverviewReport> OverviewReport { get; set; } = new List<OverviewReport>();

    public virtual UserProjects UpUu { get; set; } = null!;
}
