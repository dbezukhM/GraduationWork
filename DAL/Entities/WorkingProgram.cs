namespace DAL.Entities
{
    public class WorkingProgram : BaseEntity
    {
        public string FileName { get; set; }

        public Guid CreatedById { get; set; }

        public Guid? ApprovedById { get; set; }

        public Person CreatedBy { get; set; }

        public Person ApprovedBy { get; set; }
    }
}