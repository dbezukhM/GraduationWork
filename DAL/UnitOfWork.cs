using DAL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EducationalProgramsDbContext _educationalProgramsDbContext;
        private readonly WorkingProgramsDbContext _workingProgramsDbContext;

        public UnitOfWork(EducationalProgramsDbContext context, WorkingProgramsDbContext workingProgramsDbContext)
        {
            _educationalProgramsDbContext = context;
            _workingProgramsDbContext = workingProgramsDbContext;
        }

        public async Task SaveChangesAsync()
        {
            await SaveChangesAsync(_educationalProgramsDbContext);
            await SaveChangesAsync(_workingProgramsDbContext);
        }

        public async Task<T> NewTransaction<T>(Func<Task<T>> action)
        {
            var t = await action();
            await SaveChangesAsync();
            return t;
        }

        private static async Task SaveChangesAsync(DbContext context)
        {
            context.ChangeTracker.DetectChanges();

            var entitiesToTrack = context
                .ChangeTracker
                .Entries()
                .Where(e => e.State != EntityState.Detached && e.State != EntityState.Unchanged)
                .ToList();

            if (entitiesToTrack.Any())
            {
                await context.SaveChangesAsync();
            }
        }
    }
}