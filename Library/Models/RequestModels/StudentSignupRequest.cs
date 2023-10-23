namespace Library.Models.RequestModels
{
    public class StudentSignupRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Password { get; set; }
        public int FacultyId { get; set; }
        public string AcademicYear { get; set; }
        public string Email { get; set; }
    }
}
