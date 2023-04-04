namespace BLL.Models
{
    public class RejectModel : IDomainModel
    {
        public Guid WorkingProgramId { get; set; }

        public string Reason { get; set; }
    }
}