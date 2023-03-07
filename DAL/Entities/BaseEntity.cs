namespace DAL.Entities
{
    public abstract class BaseEntity : IDbModel
    {
        public Guid Id { get; set; }
    }

    public interface IDbModel
    {
    }
}