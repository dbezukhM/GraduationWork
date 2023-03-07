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

        public DbSet<WorkingProgram>? WorkingPrograms { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<WorkingProgram>()
                .HasOne<Person>(p => p.CreatedBy)
                .WithMany(p => p.WorkingProgramsAuthors)
                .HasForeignKey(p => p.CreatedById);

            builder.Entity<WorkingProgram>()
                .HasOne<Person>(p => p.ApprovedBy)
                .WithMany(p => p.WorkingProgramsApprovers)
                .HasForeignKey(p => p.ApprovedById);

            base.OnModelCreating(builder);
        }
    }
}