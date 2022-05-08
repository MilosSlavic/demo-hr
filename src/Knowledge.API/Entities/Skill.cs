namespace Knowledge.API.Entities
{
    public class Skill
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public byte Grade { get; set; }
    }
}
