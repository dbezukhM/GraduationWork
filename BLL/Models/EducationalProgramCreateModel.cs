namespace BLL.Models
{
    public class EducationalProgramCreateModel : IDomainModel
    {
        public string Name { get; set; }

        public Guid FacultyId { get; set; }

        public Guid SpecializationId { get; set; }

        public Guid EducationalProgramsTypeId { get; set; }
    }
}