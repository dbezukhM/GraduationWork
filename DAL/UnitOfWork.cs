using DAL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EducationalProgramsDbContext _educationalProgramsDbContext;

        public UnitOfWork(EducationalProgramsDbContext context)
        {
            _educationalProgramsDbContext = context;
        }

        public async Task SaveChangesAsync()
        {
            await SaveEpChangesAsync();
        }

        private async Task SaveEpChangesAsync()
        {
            _educationalProgramsDbContext.ChangeTracker.DetectChanges();

            var entitiesToTrack = _educationalProgramsDbContext
                .ChangeTracker
                .Entries()
                .Where(e => e.State != EntityState.Detached && e.State != EntityState.Unchanged)
                .ToList();

            if (entitiesToTrack.Any())
            {
                await _educationalProgramsDbContext.SaveChangesAsync();
            }
        }
    }
}