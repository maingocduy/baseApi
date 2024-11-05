using System;
using System.Collections.Generic;

namespace TaskMonitor.Databases.TM;

public partial class ContractorCat
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string? Note { get; set; }

    public DateTime? Updated { get; set; }

    public DateTime Created { get; set; }

    /// <summary>
    /// 0 - đang khóa, 1 - hoạt động
    /// </summary>
    public sbyte Status { get; set; }

    public sbyte? IsDefault { get; set; }

    public virtual ICollection<Contractor> Contractor { get; set; } = new List<Contractor>();
}
