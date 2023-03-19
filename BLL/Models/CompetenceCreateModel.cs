namespace BLL.Models
{
    public class CompetenceCreateModel : IDomainModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid EducationalProgramId { get; set; }
    }
}