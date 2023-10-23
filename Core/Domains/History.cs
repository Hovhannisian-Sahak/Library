using System;
using System.Collections.Generic;

namespace Core.Domains;

public partial class History
{
    public int Id { get; set; }

    public int? StudentId { get; set; }

    public int? BookId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? BorrowDate { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public bool IsReturned { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Employee? Employee { get; set; }
}
