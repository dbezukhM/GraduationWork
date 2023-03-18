namespace BLL.Models
{
    public class EducationalProgramUpdateModel : IDomainModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid FacultyId { get; set; }

        public Guid SpecializationId { get; set; }

        public Guid EducationalProgramsTypeId { get; set; }
    }
}