namespace DAL.Entities
{
    public class University : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Faculty> Faculties { get; set; }
    }
}