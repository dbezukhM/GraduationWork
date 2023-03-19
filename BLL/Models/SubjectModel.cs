namespace BLL.Models
{
    public class SubjectModel : IDomainModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SelectiveBlockName { get; set; }

        public string EducationalProgramName { get; set; }
    }
}