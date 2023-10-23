namespace Library.Models.RequestModels
{
    public class AddEmployeeRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public int ProfessionId { get; set; }
    }
}
