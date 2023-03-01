namespace DAL.Entities
{
    //галузі знань
    public class AreaOfExpertise : BaseEntity
    {
        public string? Name { get; set; }

        public ICollection<Specialization>? Specializations { get; set; }
    }
}