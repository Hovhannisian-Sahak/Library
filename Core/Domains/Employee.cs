using System;
using System.Collections.Generic;

namespace Core.Domains;

public partial class Employee
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime HireDate { get; set; }

    public int Salary { get; set; }

    public int ProfessionId { get; set; }

    public string? Email { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<EmployeePassword> EmployeePasswords { get; set; } = new List<EmployeePassword>();

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual Profession Profession { get; set; } = null!;
}
