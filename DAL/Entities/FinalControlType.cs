namespace DAL.Entities
{
    public class FinalControlType : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Subject> Subjects { get; set; }
    }
}