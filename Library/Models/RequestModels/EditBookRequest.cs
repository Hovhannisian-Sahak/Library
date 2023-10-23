using Microsoft.AspNetCore.Mvc;

namespace Library.Models.RequestModels
{
    public class EditBookRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; } = null;
        public string? Author { get; set; }  = null;
        public int? Count { get; set; } = null;

    }
}
