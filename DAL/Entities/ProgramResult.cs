namespace DAL.Entities
{
    public class ProgramResult : BaseEntity
    {
        public string? Description { get; set; }

        public Guid EducationalProgramId { get; set; }

        public EducationalProgram? EducationalProgram { get; set; }

        public ICollection<SubjectProgramResult>? SubjectProgramResults { get; set; }
    }
}