namespace DAL.Contracts
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}