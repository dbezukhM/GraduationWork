namespace BLL.Models
{
    public class SpecializationModel : IDomainModel
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public string Name { get; set; }

        public Guid AreaOfExpertiseId { get; set; }
    }
}