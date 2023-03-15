using DAL.DatabaseInitializers;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class EducationalProgramsDbContext : DbContext
    {
        public EducationalProgramsDbContext(DbContextOptions<EducationalProgramsDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<FinalControlType>? FinalControlTypes { get; set; }
        public DbSet<Subject>? Subjects { get; set; }
        public DbSet<SubjectProgramResult>? SubjectProgramResults { get; set; }
        public DbSet<ProgramResult>? ProgramResults { get; set; }
        public DbSet<SelectiveBlock>? SelectiveBlocks { get; set; }
        public DbSet<SubjectCompetence>? SubjectCompetences { get; set; }
        public DbSet<Competence>? Competences { get; set; }
        public DbSet<AreaOfExpertise>? AreaOfExpertise { get; set; }
        public DbSet<Specialization>? Specializations { get; set; }
        public DbSet<EducationalProgram>? EducationalPrograms { get; set; }
        public DbSet<University>? Universities { get; set; }
        public DbSet<Faculty>? Faculties { get; set; }
        public DbSet<EducationalProgramsType>? EducationalProgramsTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DatabaseSeeder.Seed(modelBuilder);
        }
    }
}