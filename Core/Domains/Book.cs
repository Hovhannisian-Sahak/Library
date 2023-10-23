using System;
using System.Collections.Generic;

namespace Core.Domains;

public partial class Book
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Author { get; set; } = null!;

    public DateTime PublishDate { get; set; }

    public int Count { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();
}
