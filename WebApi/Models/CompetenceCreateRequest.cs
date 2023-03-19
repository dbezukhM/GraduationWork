namespace WebApi.Models
{
    public class CompetenceCreateRequest : IRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid EducationalProgramId { get; set; }
    }
}