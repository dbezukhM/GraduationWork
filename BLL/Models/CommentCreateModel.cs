namespace BLL.Models
{
    public class CommentCreateModel : IDomainModel
    {
        public string Description { get; set; }

        public string CreatedByEmail { get; set; }

        public Guid WorkingProgramId { get; set; }
    }
}