using System;
using System.Collections.Generic;

namespace BaseApi.Databases.TM;

public partial class Notify
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public sbyte Status { get; set; }

    public sbyte Type { get; set; }

    public virtual ICollection<NotifyAcc> NotifyAcc { get; set; } = new List<NotifyAcc>();
}
