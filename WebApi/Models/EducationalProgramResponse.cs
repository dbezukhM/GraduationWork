namespace WebApi.Models
{
    public class EducationalProgramResponse : IResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UniversityName { get; set; }

        public string SpecializationName { get; set; }

        public string EducationalProgramsTypeName { get; set; }
    }
}