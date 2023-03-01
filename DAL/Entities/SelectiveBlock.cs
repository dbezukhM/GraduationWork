namespace DAL.Entities
{
    public class SelectiveBlock : BaseEntity
    {
        public string? Name { get; set; }

        public Guid EducationalProgramId { get; set; }

        public EducationalProgram? EducationalProgram { get; set; }

        public ICollection<Subject>? Subjects { get; set; }
    }
}