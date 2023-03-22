using BLL.Models;
using DAL.Entities;

namespace WebApi.Models
{
    public class PersonGetResponse : IResponse
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsFirstPasswordChanged { get; set; }

        public bool IsAdmin { get; set; }

        public IEnumerable<IdNameModel<Guid>> WorkingProgramsAuthor { get; set; }

        public IEnumerable<IdNameModel<Guid>> WorkingProgramsApprover { get; set; }
    }
}