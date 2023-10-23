using System;
using System.Collections.Generic;

namespace Core.Domains;

public partial class EmployeePassword
{
    public int Id { get; set; }

    public string? HashedPassword { get; set; }

    public DateTime CreatedAt { get; set; }

    public int EmployeeId { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
