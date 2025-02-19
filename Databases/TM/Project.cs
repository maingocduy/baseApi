using System;
using System.Collections.Generic;

namespace BaseApi.Databases.TM;

public partial class Project
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string? Code { get; set; }

    public string BrachUuid { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Matp { get; set; }

    public string? Maqh { get; set; }

    public string? Xaid { get; set; }

    public string Address { get; set; } = null!;

    public string? Description { get; set; }

    public sbyte Priority { get; set; }

    public DateTime? ExpectStart { get; set; }

    public DateTime? ExpectEnd { get; set; }

    public DateTime? RealStart { get; set; }

    public DateTime? RealEnd { get; set; }

    /// <summary>
    /// Kế hoạch vốn đầu tư (PlanBudget)
    /// </summary>
    public double? ExpectBudget { get; set; }

    /// <summary>
    /// Tổng mức đầu tư dự án (ProjectBudget)
    /// </summary>
    public double? TotalInvest { get; set; }

    /// <summary>
    /// Tổng dự toán
    /// </summary>
    public double? RealBudget { get; set; }

    /// <summary>
    /// 1 - chuẩn bị , 2 - thực hiện , 3 kết thúc 
    /// </summary>
    public sbyte State { get; set; }

    public sbyte Type { get; set; }

    public double? AccumAmount { get; set; }

    public double? AccumReserve { get; set; }

    public double? ReserveBudget { get; set; }

    public DateTime Updated { get; set; }

    public DateTime Created { get; set; }

    /// <summary>
    /// 0 - đang khóa, 1 - hoạt động
    /// </summary>
    public sbyte Status { get; set; }

    public virtual ICollection<Activity> Activity { get; set; } = new List<Activity>();

    public virtual Branches BrachUu { get; set; } = null!;

    public virtual DevvnQuanhuyen? MaqhNavigation { get; set; }

    public virtual DevvnTinhthanhpho? MatpNavigation { get; set; }

    public virtual ICollection<ProjectAnnual> ProjectAnnual { get; set; } = new List<ProjectAnnual>();

    public virtual ICollection<ProjectContractor> ProjectContractor { get; set; } = new List<ProjectContractor>();

    public virtual ICollection<ProjectFund> ProjectFund { get; set; } = new List<ProjectFund>();

    public virtual ICollection<UserProjects> UserProjects { get; set; } = new List<UserProjects>();

    public virtual DevvnXaphuongthitran? Xa { get; set; }
}
