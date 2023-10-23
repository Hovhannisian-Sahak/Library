using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AddEmployeeModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public int ProfessionId { get; set; }
    }
}
