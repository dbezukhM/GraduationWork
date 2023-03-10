namespace BLL.Models
{
    public class WorkingProgramGetModel : IDomainModel
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public Guid CreatedById { get; set; }

        public Guid? ApprovedById { get; set; }
    }
}