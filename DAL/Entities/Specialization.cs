namespace DAL.Entities
{
    public class Specialization : BaseEntity
    {
        public string? Name { get; set; }

        public Guid AreaOfExpertiseId { get; set; }

        public AreaOfExpertise? AreaOfExpertise { get; set; }

        public ICollection<EducationalProgram>? EducationalPrograms { get; set; }
    }
}