namespace DAL.Entities
{
    public class Competence : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? EducationalProgramId { get; set; }

        public EducationalProgram EducationalProgram { get; set; }

        public ICollection<SubjectCompetence> SubjectCompetences { get; set; }
    }
}