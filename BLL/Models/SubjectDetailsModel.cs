using DAL.Entities;

namespace BLL.Models
{
    public class SubjectDetailsModel
    {
        public Subject Subject { get; set; }

        public University University { get; set; }

        public Faculty Faculty { get; set; }

        public AreaOfExpertise AreaOfExpertise { get; set; }

        public Specialization Specialization { get; set; }

        public EducationalProgramsType EducationalProgramsType { get; set; }

        public EducationalProgram EducationalProgram { get; set; }

        public SelectiveBlock SelectiveBlock { get; set; }

        public FinalControlType FinalControlType { get; set; }

        public IEnumerable<Competence> Competences { get; set; }

        public IEnumerable<ProgramResult> ProgramResults { get; set; }
    }
}