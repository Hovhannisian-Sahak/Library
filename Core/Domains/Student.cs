using System;
using System.Collections.Generic;

namespace Core.Domains;

public partial class Student
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public int FacultyId { get; set; }

    public string AcademicYear { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? Email { get; set; }

    public virtual Faculty Faculty { get; set; } = null!;

    public virtual ICollection<StudentPassword> StudentPasswords { get; set; } = new List<StudentPassword>();
}
