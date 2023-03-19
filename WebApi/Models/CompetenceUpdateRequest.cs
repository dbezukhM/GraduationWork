namespace WebApi.Models
{
    public class CompetenceUpdateRequest : IRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid EducationalProgramId { get; set; }
    }
}