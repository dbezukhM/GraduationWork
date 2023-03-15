namespace DAL.Entities
{
    public class SelectiveBlock : BaseEntity
    {
        public string? Name { get; set; }

        public ICollection<Subject>? Subjects { get; set; }
    }
}