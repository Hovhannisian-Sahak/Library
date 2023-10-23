namespace Library.Models.RequestModels
{
    public class FilterEmployeesRequest
    {
        public string? SearchText { get; set; } = null;
        public int? ProfessionId { get; set; } = null;
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
    }
}
