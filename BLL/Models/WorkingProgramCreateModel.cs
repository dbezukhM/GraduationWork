namespace BLL.Models
{
    public class WorkingProgramCreateModel : IDomainModel
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }
    }
}