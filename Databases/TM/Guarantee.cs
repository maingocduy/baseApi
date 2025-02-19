using System;
using System.Collections.Generic;

namespace BaseApi.Databases.TM;

public partial class Guarantee
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string PcUuid { get; set; } = null!;

    public double Amount { get; set; }

    public DateTime EndDate { get; set; }

    /// <summary>
    /// 1 - Bảo lãnh dự án, 2 - Bảo lãnh giải ngân
    /// </summary>
    public sbyte? Type { get; set; }

    public DateTime Created { get; set; }

    /// <summary>
    /// 0 - đang khóa, 1 - hoạt động
    /// </summary>
    public sbyte Status { get; set; }

    public virtual ProjectContractor PcUu { get; set; } = null!;
}
