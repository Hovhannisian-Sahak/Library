namespace Library.Models.RequestModels
{
    public class EditEmployeeRequest
    {
        public int Id { get; set; }
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public int? Salary { get; set; } = null;
        public int? ProfessionId { get; set; } = null;
    }
}
