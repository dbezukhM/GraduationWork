namespace WebApi.Models
{
    public class UniversityResponse : IResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}