using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class WorkingProgramsDbContext : IdentityDbContext<Person, IdentityRole<Guid>, Guid>
    {
        public WorkingProgramsDbContext(DbContextOptions<WorkingProgramsDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<WorkingProgram> WorkingPrograms { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<WorkingProgram>()
                .HasOne<Person>(p => p.CreatedBy)
                .WithMany(p => p.WorkingProgramsAuthor)
                .HasForeignKey(p => p.CreatedById);

            builder.Entity<WorkingProgram>()
                .HasOne<Person>(p => p.ApprovedBy)
                .WithMany(p => p.WorkingProgramsApprover)
                .HasForeignKey(p => p.ApprovedById);

            builder.Entity<Comment>()
                .HasOne<Person>(p => p.CreatedBy)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
                .HasOne<WorkingProgram>(p => p.WorkingProgram)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.WorkingProgramId);

            base.OnModelCreating(builder);
        }
    }
}