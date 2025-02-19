using System;
using System.Collections.Generic;

namespace BaseApi.Databases.TM;

public partial class Branches
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string? Matp { get; set; }

    public string? Maqh { get; set; }

    public string? Xaid { get; set; }

    public string? Address { get; set; }

    public string? Note { get; set; }

    public DateTime? Updated { get; set; }

    public DateTime Created { get; set; }

    /// <summary>
    /// 0-không sử dụng , 1- sử dụng
    /// </summary>
    public sbyte Status { get; set; }

    public virtual DevvnQuanhuyen? MaqhNavigation { get; set; }

    public virtual DevvnTinhthanhpho? MatpNavigation { get; set; }

    public virtual ICollection<Project> Project { get; set; } = new List<Project>();

    public virtual DevvnXaphuongthitran? Xa { get; set; }
}
