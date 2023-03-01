namespace DAL.Entities
{
    public class Competence : BaseEntity
    {
        public string? Description { get; set; }

        public Guid CompetenceTypeId { get; set; }

        public CompetenceType? CompetenceType { get; set; }

        public ICollection<EducationalProgramsCompetence>? EducationalProgramsCompetences { get; set; }
    }
}