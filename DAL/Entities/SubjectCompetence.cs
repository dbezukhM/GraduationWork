namespace DAL.Entities
{
    public class SubjectCompetence : BaseEntity
    {
        public Guid SubjectId { get; set; }

        public Guid CompetenceId { get; set; }

        public Subject? Subject { get; set; }

        public Competence? Competence { get; set; }
    }
}