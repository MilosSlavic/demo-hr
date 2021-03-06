using Employee.API.Enums;

namespace Employee.API
{
    public class Employee
    {
        public int Id { get; set; }
        public string PersonalIdentificationNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public TitleOption Title { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }
    }
}
