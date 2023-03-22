using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class Person : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsFirstPasswordChanged { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<WorkingProgram> WorkingProgramsAuthor { get; set; }

        public ICollection<WorkingProgram> WorkingProgramsApprover { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}