using System;
using System.Collections.Generic;

namespace TaskMonitor.Databases.TM;

public partial class Activity
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string? TaskUuid { get; set; }

    public string ProjectUuid { get; set; } = null!;

    /// <summary>
    /// để biết công việc phát sinh thuộc công việc nào
    /// </summary>
    public string? ParentTaskUuid { get; set; }

    public string? Name { get; set; }

    public string? Note { get; set; }

    public sbyte? Priority { get; set; }

    /// <summary>
    /// 0 - chưa xử lý , 1 - đang xử lý , 2 - hoàn thành
    /// </summary>
    public sbyte State { get; set; }

    public DateTime? Updated { get; set; }

    public DateTime Created { get; set; }

    /// <summary>
    /// 0-không sử dụng , 1- sử dụng
    /// </summary>
    public sbyte Status { get; set; }

    public virtual ICollection<ActivityReport> ActivityReport { get; set; } = new List<ActivityReport>();

    public virtual Task? ParentTaskUu { get; set; }

    public virtual Project ProjectUu { get; set; } = null!;

    public virtual Task? TaskUu { get; set; }
}
