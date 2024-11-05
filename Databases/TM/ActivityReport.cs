using System;
using System.Collections.Generic;

namespace TaskMonitor.Databases.TM;

public partial class ActivityReport
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string ReportUuid { get; set; } = null!;

    public string ActivityUuid { get; set; } = null!;

    /// <summary>
    /// 0 - chưa xử lý , 1 - đang xử lý , 2 - hoàn thành
    /// </summary>
    public sbyte State { get; set; }

    /// <summary>
    /// Trạng thái số hóa: 0 - Chưa số hóa, 1 - Đã số hóa
    /// </summary>
    public sbyte StateNote { get; set; }

    public int? Progress { get; set; }

    public string? Issue { get; set; }

    public DateTime? ExpectStart { get; set; }

    public DateTime? ExpectEnd { get; set; }

    public DateTime? RealStart { get; set; }

    public DateTime? RealEnd { get; set; }

    public DateTime? Completed { get; set; }

    public DateTime Created { get; set; }

    /// <summary>
    /// 0-không sử dụng , 1- sử dụng
    /// </summary>
    public sbyte Status { get; set; }

    public virtual Activity ActivityUu { get; set; } = null!;

    public virtual Reports ReportUu { get; set; } = null!;
}
