using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class EditEmployeeModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public int? Salary { get; set; } = null;
        public int? ProfessionId { get; set; } = null;
    }
}
