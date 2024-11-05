using System;
using System.Collections.Generic;

namespace TaskMonitor.Databases.TM;

public partial class DevvnTinhthanhpho
{
    public string Matp { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Slug { get; set; }

    public virtual ICollection<Branches> Branches { get; set; } = new List<Branches>();

    public virtual ICollection<Contractor> Contractor { get; set; } = new List<Contractor>();

    public virtual ICollection<Project> Project { get; set; } = new List<Project>();

    public virtual ICollection<User> User { get; set; } = new List<User>();
}
