using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class StudentsWithLateReturn
    {
        public int? Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int? FacultyId { get; set; }

        public string? AcademicYear { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
