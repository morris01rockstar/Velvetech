using Microsoft.EntityFrameworkCore;
using Velvetech.Domain.Entities;

namespace Velvetech.Persistense
{
	public class ApplicationDbContext : DbContext
  {
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<StudentGroup>()
          .HasKey(t => new { t.StudentId, t.GroupId });

      modelBuilder.Entity<StudentGroup>()
          .HasOne(sc => sc.Student)
          .WithMany(s => s.StudentGroups)
          .HasForeignKey(sc => sc.StudentId);

      modelBuilder.Entity<StudentGroup>()
          .HasOne(sc => sc.Group)
          .WithMany(c => c.StudentGroups)
          .HasForeignKey(sc => sc.GroupId);

      modelBuilder.Entity<Student>()
        .HasIndex(s => s.Identifier)
        .IsUnique();
    }
  }
}
