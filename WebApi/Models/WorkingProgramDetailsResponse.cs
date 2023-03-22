using BLL.Models;

namespace WebApi.Models
{
    public class WorkingProgramDetailsResponse : IResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IdNameModel<Guid> Subject { get; set; }

        public IdNameModel<Guid> EducationalProgram { get; set; }

        public string CreatedByName { get; set; }

        public string ApprovedByName { get; set; }

        public bool IsAvailable { get; set; }

        public IEnumerable<CommentResponse> Comments { get; set; }
    }
}