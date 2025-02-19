using System;
using System.Collections.Generic;

namespace BaseApi.Databases.TM;

public partial class Otp
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string? AccUuid { get; set; }

    public string Otp1 { get; set; } = null!;

    public DateTime Expired { get; set; }

    public DateTime? Updated { get; set; }

    public DateTime Created { get; set; }

    public sbyte State { get; set; }

    /// <summary>
    /// 0-không sử dụng , 1- sử dụng 
    /// </summary>
    public sbyte Status { get; set; }

    public virtual Account? AccUu { get; set; }
}
