using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class EducationalProgramUpdateRequest : IRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Guid FacultyId { get; set; }

        [Required]
        public Guid SpecializationId { get; set; }

        [Required]
        public Guid EducationalProgramsTypeId { get; set; }
    }
}