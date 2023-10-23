namespace Library.Models.RequestModels
{
    public class AddBookRequest
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public int Count { get; set; }
    }
}
