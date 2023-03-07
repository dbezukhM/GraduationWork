namespace BLL.Models
{
    public class UniversityModel : IDomainModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}