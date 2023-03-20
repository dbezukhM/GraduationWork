namespace BLL.Models
{
    public class WorkingProgramModel : IDomainModel
    {
        public string FullFileName { get; set; }

        public MemoryStream File { get; set; }
    }
}