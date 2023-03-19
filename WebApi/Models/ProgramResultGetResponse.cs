using BLL.Models;

namespace WebApi.Models
{
    public class ProgramResultGetResponse : IResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IdNameModel<Guid> EducationalProgram { get; set; }

        public IEnumerable<IdNameModel<Guid>> Subjects { get; set; }
    }
}