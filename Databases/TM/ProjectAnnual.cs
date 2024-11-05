using System;
using System.Collections.Generic;

namespace TaskMonitor.Databases.TM;

public partial class ProjectAnnual
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public int Year { get; set; }

    public double Budget { get; set; }

    public double AccumAmount { get; set; }

    public DateTime Updated { get; set; }

    public DateTime Created { get; set; }

    /// <summary>
    /// 0 - đang khóa, 1 - hoạt động
    /// </summary>
    public sbyte Status { get; set; }

    public string ProjectUuid { get; set; } = null!;

    public virtual Project ProjectUu { get; set; } = null!;
}
