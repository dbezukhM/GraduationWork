namespace BLL.Models
{
    public class EducationalProgramGetModel : IDomainModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IdNameModel<Guid> AreaOfExpertise { get; set; }

        public IdNameModel<Guid> Specialization { get; set; }

        public IdNameModel<Guid> University { get; set; }

        public IdNameModel<Guid> Faculty { get; set; }

        public IdNameModel<Guid> EducationalProgramsType { get; set; }

        public IEnumerable<IdNameModel<Guid>> Subjects { get; set; }

        public IEnumerable<CompetenceModel> Competences { get; set; }

        public IEnumerable<ProgramResultModel> ProgramResults { get; set; }
    }
}