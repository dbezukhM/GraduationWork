namespace BLL.Models
{
    public class CommentModel : IDomainModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string CreatedByName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}