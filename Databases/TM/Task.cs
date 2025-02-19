using System;
using System.Collections.Generic;

namespace BaseApi.Databases.TM;

public partial class Task
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string? ParentUuid { get; set; }

    public string Name { get; set; } = null!;

    public string? Note { get; set; }

    public sbyte? Priority { get; set; }

    public int Order { get; set; }

    public int Type { get; set; }

    /// <summary>
    /// 1-Chuẩn bị  , 2- Thực hiện , 3- kết thúc
    /// </summary>
    public sbyte? Stage { get; set; }

    public DateTime? Updated { get; set; }

    public DateTime Created { get; set; }

    /// <summary>
    /// 0-không sử dụng , 1- sử dụng
    /// </summary>
    public sbyte Status { get; set; }

    public virtual ICollection<Activity> ActivityParentTaskUu { get; set; } = new List<Activity>();

    public virtual ICollection<Activity> ActivityTaskUu { get; set; } = new List<Activity>();

    public virtual ICollection<Task> InverseParentUu { get; set; } = new List<Task>();

    public virtual Task? ParentUu { get; set; }

    public virtual TaskCat TypeNavigation { get; set; } = null!;
}
