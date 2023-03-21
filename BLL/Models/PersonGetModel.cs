using DAL.Entities;

namespace BLL.Models
{
    public class PersonGetModel : IDomainModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsFirstPasswordChanged { get; set; }

        public bool IsAdmin { get; set; }

        public IEnumerable<WorkingProgram> WorkingProgramsAuthor { get; set; }

        public IEnumerable<WorkingProgram> WorkingProgramsApprover { get; set; }
    }
}