using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class StudentsMostReadBooks
    {
        public int? Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? MiddleName { get; set; }

        public int? FacultyId { get; set; }

        public string? AcademicYear { get; set; }

        public int? ReadBookCount { get; set; }
    }
}
