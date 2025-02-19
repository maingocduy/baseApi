using System;
using System.Collections.Generic;

namespace BaseApi.Databases.TM;

public partial class Contractor
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string? Matp { get; set; }

    public string? Maqh { get; set; }

    public string? Xaid { get; set; }

    public string? Address { get; set; }

    public int Type { get; set; }

    public string? Note { get; set; }

    public DateTime? Updated { get; set; }

    public DateTime Created { get; set; }

    /// <summary>
    /// 0 - đang khóa, 1 - hoạt động
    /// </summary>
    public sbyte Status { get; set; }

    public virtual DevvnQuanhuyen? MaqhNavigation { get; set; }

    public virtual DevvnTinhthanhpho? MatpNavigation { get; set; }

    public virtual ICollection<ProjectContractor> ProjectContractor { get; set; } = new List<ProjectContractor>();

    public virtual ContractorCat TypeNavigation { get; set; } = null!;

    public virtual DevvnXaphuongthitran? Xa { get; set; }
}
