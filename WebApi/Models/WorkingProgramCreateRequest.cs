namespace WebApi.Models
{
    public class WorkingProgramCreateRequest : IRequest
    {
        public Guid SubjectId { get; set; }

        public string Name { get; set; }

        public IFormFile File { get; set; }
    }
}