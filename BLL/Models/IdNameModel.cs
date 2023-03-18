namespace BLL.Models
{
    public class IdNameModel<T> : IDomainModel
    {
        public string Name { get; set; }

        public T Id { get; set; }
    }
}