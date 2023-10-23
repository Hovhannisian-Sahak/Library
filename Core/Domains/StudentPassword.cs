using System;
using System.Collections.Generic;

namespace Core.Domains;

public partial class StudentPassword
{
    public int Id { get; set; }

    public string? HashedPassword { get; set; }

    public DateTime CreatedAt { get; set; }

    public int StudentId { get; set; }

    public virtual Student Student { get; set; } = null!;
}
