using System;
using System.Collections.Generic;

namespace BaseApi.Databases.TM;

public partial class UserProjects
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string ProjectUuid { get; set; } = null!;

    public string UserUuid { get; set; } = null!;

    /// <summary>
    /// 1 - Nhân viên, 2 - PM
    /// </summary>
    public sbyte Role { get; set; }

    public DateTime Created { get; set; }

    /// <summary>
    /// 0-không sử dụng , 1- sử dụng
    /// </summary>
    public sbyte Status { get; set; }

    public virtual Project ProjectUu { get; set; } = null!;

    public virtual ICollection<Reports> Reports { get; set; } = new List<Reports>();

    public virtual User UserUu { get; set; } = null!;
}
