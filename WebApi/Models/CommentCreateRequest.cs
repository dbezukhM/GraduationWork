namespace WebApi.Models
{
    public class CommentCreateRequest : IRequest
    {
        public string Description { get; set; }

        public Guid WorkingProgramId { get; set; }
    }
}