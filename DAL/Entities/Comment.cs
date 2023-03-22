namespace DAL.Entities
{
    public class Comment : BaseEntity
    {
        public string Description { get; set; }

        public Guid CreatedById { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid WorkingProgramId { get; set; }

        public WorkingProgram WorkingProgram { get; set; }

        public Person CreatedBy { get; set; }
    }
}