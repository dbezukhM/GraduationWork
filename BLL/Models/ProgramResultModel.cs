namespace BLL.Models
{
    public class ProgramResultModel : IDomainModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? EducationalProgramId { get; set; }
    }
}