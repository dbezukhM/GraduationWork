namespace DAL.Entities
{
    public class SubjectProgramResult : BaseEntity
    {
        public Guid ProgramResultId { get; set; }

        public Guid SubjectId { get; set; }

        public ProgramResult ProgramResult { get; set; }

        public Subject Subject { get; set; }
    }
}