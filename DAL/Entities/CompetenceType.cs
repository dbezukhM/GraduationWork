namespace DAL.Entities
{
    public class CompetenceType : BaseEntity
    {
        public string? Name { get; set; }

        public ICollection<Competence>? Competences { get; set; }
    }
}