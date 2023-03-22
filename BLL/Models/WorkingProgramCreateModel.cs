using Microsoft.AspNetCore.Http;

namespace BLL.Models
{
    public class WorkingProgramCreateModel : IDomainModel
    {
        public string Name { get; set; }

        public IFormFile File { get; set; }

        public Guid SubjectId { get; set; }

        public string CreatedByEmail { get; set; }
    }
}