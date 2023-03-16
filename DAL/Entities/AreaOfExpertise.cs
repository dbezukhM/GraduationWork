namespace DAL.Entities
{
    //галузі знань
    public class AreaOfExpertise : BaseEntity
    {
        public int Number { get; set; }

        public string Name { get; set; }

        public ICollection<Specialization> Specializations { get; set; }
    }
}