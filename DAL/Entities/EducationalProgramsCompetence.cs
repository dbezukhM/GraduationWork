namespace DAL.Entities
{
    public class EducationalProgramsCompetence : BaseEntity
    {
        public Guid EducationalProgramId { get; set; }

        public Guid CompetenceId { get; set; }

        public EducationalProgram? EducationalProgram { get; set; }

        public Competence? Competence { get; set; }

        public ICollection<SubjectCompetence>? SubjectCompetences { get; set; }
    }
}