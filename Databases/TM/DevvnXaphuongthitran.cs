using System;
using System.Collections.Generic;

namespace BaseApi.Databases.TM;

public partial class DevvnXaphuongthitran
{
    public string Xaid { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Maqh { get; set; } = null!;

    public virtual ICollection<Branches> Branches { get; set; } = new List<Branches>();

    public virtual ICollection<Contractor> Contractor { get; set; } = new List<Contractor>();

    public virtual ICollection<Project> Project { get; set; } = new List<Project>();

    public virtual ICollection<User> User { get; set; } = new List<User>();
}
