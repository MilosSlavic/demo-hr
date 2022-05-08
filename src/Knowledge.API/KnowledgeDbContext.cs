using Microsoft.EntityFrameworkCore;

namespace Knowledge.API
{
    public class KnowledgeDbContext : DbContext
    {
        public KnowledgeDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entities.Skill> Skills { get; set; }

        public DbSet<Entities.Certificate> Certificates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var skillEntity = modelBuilder.Entity<Entities.Skill>();
            skillEntity.ToTable(nameof(Entities.Skill));
            skillEntity.HasKey(x => x.Id);
            skillEntity.Property(x => x.Name).HasMaxLength(50);

            var certificateEntity = modelBuilder.Entity<Entities.Certificate>();
            certificateEntity.ToTable(nameof(Entities.Certificate));
            certificateEntity.HasKey(x => x.Id);
            certificateEntity.Property(x => x.Name).HasMaxLength(128);

            base.OnModelCreating(modelBuilder);
        }
    }
}
