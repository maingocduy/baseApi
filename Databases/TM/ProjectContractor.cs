using System;
using System.Collections.Generic;

namespace TaskMonitor.Databases.TM;

public partial class ProjectContractor
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string? ProjectUuid { get; set; }

    public string ContractorUuid { get; set; } = null!;

    public DateTime Created { get; set; }

    /// <summary>
    /// 0 - đang khóa, 1 - hoạt động
    /// </summary>
    public sbyte Status { get; set; }

    /// <summary>
    /// Thời gian hết hạn hợp đồng
    /// </summary>
    public DateTime? ContractEndDate { get; set; }

    /// <summary>
    /// Giá trị hợp đồng
    /// </summary>
    public double? ContractAmount { get; set; }

    public virtual Contractor ContractorUu { get; set; } = null!;

    public virtual ICollection<Guarantee> Guarantee { get; set; } = new List<Guarantee>();

    public virtual Project? ProjectUu { get; set; }
}
