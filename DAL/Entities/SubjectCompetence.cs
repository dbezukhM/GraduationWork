namespace DAL.Entities
{
    public class SubjectCompetence : BaseEntity
    {
        public Guid? SubjectId { get; set; }

        public Guid EducationalProgramsCompetenceId { get; set; }

        public Subject? Subject { get; set; }

        public EducationalProgramsCompetence? EducationalProgramsCompetence { get; set; }
    }
}