using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class EmployeesWithBooks
    {
        public int? Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int? ProfessionId { get; set; }

        public int? Salary { get; set; }

        public string? OfferedBooks { get; set; }
    }
}
