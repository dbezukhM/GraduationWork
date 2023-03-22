namespace WebApi.Models
{
    public class WorkingProgramGetResponse : IResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SubjectName { get; set; }

        public bool IsAvailable { get; set; }
    }
}