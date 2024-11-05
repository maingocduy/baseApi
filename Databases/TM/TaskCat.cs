using System;
using System.Collections.Generic;

namespace TaskMonitor.Databases.TM;

public partial class TaskCat
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public DateTime Created { get; set; }

    /// <summary>
    /// 0-không sử dụng , 1- sử dụng , 2-khóa
    /// </summary>
    public sbyte Status { get; set; }

    public virtual ICollection<Task> Task { get; set; } = new List<Task>();
}
