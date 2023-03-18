namespace BLL.Models
{
    public class AreaOfExpertiseModel : IDomainModel
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public string Name { get; set; }
    }
}