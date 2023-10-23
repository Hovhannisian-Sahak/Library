using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class FilteredEmployees
    {
        public int Id { get; set; }

        public string FirstName { get; set; } 

        public string LastName { get; set; }

        public DateTime HireDate { get; set; }

        public int Salary { get; set; }

        public int ProfessionId { get; set; }

        public string? Email { get; set; }

        public int? RoleId { get; set; }

    }
}
