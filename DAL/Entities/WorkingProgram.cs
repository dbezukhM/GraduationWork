namespace DAL.Entities
{
    public class WorkingProgram : BaseEntity
    {
        public string Name { get; set; }

        public string FileName { get; set; }

        public bool IsAvailable { get; set; }

        public Guid SubjectId { get; set; }

        public Guid CreatedById { get; set; }

        public Guid? ApprovedById { get; set; }

        public Person CreatedBy { get; set; }

        public Person ApprovedBy { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}