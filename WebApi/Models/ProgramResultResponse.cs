namespace WebApi.Models
{
    public class ProgramResultResponse : IResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? EducationalProgramId { get; set; }
    }
}