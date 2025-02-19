using System;
using System.Collections.Generic;

namespace BaseApi.Databases.TM;

public partial class Role
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public DateTime Created { get; set; }

    /// <summary>
    /// 0 - đang khóa, 1 - hoạt động
    /// </summary>
    public sbyte Status { get; set; }

    public virtual ICollection<Account> Account { get; set; } = new List<Account>();
}
