namespace WebApi.Models
{
    public class RejectRequest : IRequest
    {
        public Guid WorkingProgramId { get; set; }

        public string Reason { get; set; }
    }
}