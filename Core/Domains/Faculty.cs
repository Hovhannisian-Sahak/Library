using System;
using System.Collections.Generic;

namespace Core.Domains;

public partial class Faculty
{
    public int Id { get; set; }

    public string? FacultyName { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
