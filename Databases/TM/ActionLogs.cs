using System;
using System.Collections.Generic;

namespace BaseApi.Databases.TM;

public partial class ActionLogs
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string ActUuid { get; set; } = null!;

    public string UserUuid { get; set; } = null!;

    public double? OldValue { get; set; }

    public double? NewValue { get; set; }

    public string? Reason { get; set; }

    /// <summary>
    /// 0 - Duyệt báo cáo ; 1 - Từ chối báo cáo ; 2 - Gửi báo cáo ; 3 - Duyệt báo cáo giải ngân ; 4 - Từ chối báo cáo giải ngân ; 5 - Gửi báo cáo giải ngân
    /// </summary>
    public sbyte Action { get; set; }

    public string? Note { get; set; }

    public DateTime Created { get; set; }
}
