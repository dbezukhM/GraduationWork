namespace BLL.Models
{
    public class CompetenceGetModel : IDomainModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IdNameModel<Guid> EducationalProgram { get; set; }

        public IEnumerable<IdNameModel<Guid>> Subjects { get; set; }
    }
}