namespace DAL.Entities
{
    public class Faculty : BaseEntity
    {
        public string? Name { get; set; }

        public Guid UniversityId { get; set; }

        public University? University { get; set; }

        public ICollection<EducationalProgram>? EducationalPrograms { get; set; }
    }
}