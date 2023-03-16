namespace DAL.Entities
{
    public class EducationalProgram : BaseEntity
    {
        public string Name { get; set; }

        public Guid FacultyId { get; set; }

        public Guid SpecializationId { get; set; }

        public Guid EducationalProgramsTypeId { get; set; }

        public Faculty Faculty { get; set; }

        public Specialization Specialization { get; set; }

        public EducationalProgramsType EducationalProgramsType { get; set; }

        public ICollection<ProgramResult> ProgramResults { get; set; }

        public ICollection<Competence> Competences { get; set; }

        public ICollection<Subject> Subjects { get; set; }
    }
}