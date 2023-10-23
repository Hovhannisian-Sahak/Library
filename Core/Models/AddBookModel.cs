using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AddBookModel
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public int Count { get; set; }
    }
}
