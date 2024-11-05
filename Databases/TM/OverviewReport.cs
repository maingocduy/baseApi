using System;
using System.Collections.Generic;

namespace TaskMonitor.Databases.TM;

public partial class OverviewReport
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string FundReportUuid { get; set; } = null!;

    public string ReportUuid { get; set; } = null!;

    public int? Month { get; set; }

    public int? Year { get; set; }

    public DateTime? Updated { get; set; }

    public DateTime Created { get; set; }

    /// <summary>
    /// 0-không sử dụng , 1- sử dụng , 2-khóa
    /// </summary>
    public sbyte Status { get; set; }

    public string ReporterUuid { get; set; } = null!;

    public virtual ProjectFund FundReportUu { get; set; } = null!;

    public virtual Reports ReportUu { get; set; } = null!;

    public virtual User ReporterUu { get; set; } = null!;
}
