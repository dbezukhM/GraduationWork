namespace DAL.Entities
{
    public class EducationalProgramsType : BaseEntity
    {
        public string? Name { get; set; }

        public ICollection<EducationalProgram>? EducationalPrograms { get; set; }
    }
}