using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class FilterEmployeesModel
    {
        public string? SearchText { get; set; } = null;
        public int? ProfessionId { get; set; } = null;
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
    }
}
