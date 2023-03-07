namespace DAL.Contracts
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();

        Task<T> NewTransaction<T>(Func<Task<T>> action);
    }
}