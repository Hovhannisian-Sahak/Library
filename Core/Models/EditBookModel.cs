using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{

    public class EditBookModel
    {
        public int Id { get; set; }
        public string? Name { get; set; } = null;
        public string? Author { get; set; } = null;
        public int? Count { get; set; } = null;
    }
}
