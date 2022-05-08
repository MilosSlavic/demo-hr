using Microsoft.EntityFrameworkCore;

namespace Employee.API
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var employeeEntity = modelBuilder.Entity<Employee>();
            employeeEntity.ToTable(nameof(Employee));
            employeeEntity.HasKey(x => x.Id);
            employeeEntity.Property(x => x.FirstName).HasMaxLength(50);
            employeeEntity.Property(x => x.LastName).HasMaxLength(50);
            employeeEntity.Property(x=>x.City).HasMaxLength(50);
            employeeEntity.Property(x=>x.State).HasMaxLength(50);
            employeeEntity.Property(x=>x.PersonalIdentificationNumber).HasMaxLength(20);
            employeeEntity.Property(x=>x.Country).HasMaxLength(50);
            employeeEntity.Property(x=>x.Email).HasMaxLength(256);
            employeeEntity.HasIndex(x => x.Email).IsUnique();
            employeeEntity.Property(x=>x.Title).HasColumnType("tinyint");
        }
    }
}
