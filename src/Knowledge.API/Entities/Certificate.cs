namespace Knowledge.API.Entities
{
    public class Certificate
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Percent of correct answers.
        /// </summary>
        public int Completness { get; set; }
    }
}
