using System;
using System.Collections.Generic;

namespace BaseApi.Databases.TM;

public partial class NotifyAcc
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string AccUuid { get; set; } = null!;

    public string NotifyUuid { get; set; } = null!;

    public string? Data { get; set; }

    public DateTime? Created { get; set; }

    /// <summary>
    /// 0 - chưa xem /  1- đã xem
    /// </summary>
    public int State { get; set; }

    /// <summary>
    /// 1:Đang hoạt động/0:bị khóa
    /// </summary>
    public int Status { get; set; }

    public virtual Account AccUu { get; set; } = null!;

    public virtual Notify NotifyUu { get; set; } = null!;
}
