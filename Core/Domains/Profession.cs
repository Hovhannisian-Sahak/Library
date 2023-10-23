using System;
using System.Collections.Generic;

namespace Core.Domains;

public partial class Profession
{
    public int Id { get; set; }

    public string? ProfessionName { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
