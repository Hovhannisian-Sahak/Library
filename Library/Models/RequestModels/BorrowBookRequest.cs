namespace Library.Models.RequestModels
{
    public class BorrowBookRequest
    {
        public int BookId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? date { get; set; } = null;
    }
}
