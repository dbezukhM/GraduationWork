namespace DAL.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }

        public int Credits { get; set; }

        public int Semester { get; set; }

        public int LecturesHours { get; set; }

        public int SeminarsHours { get; set; }

        public int PracticalClassesHours { get; set; }

        public int LaboratoryClassesHours { get; set; }

        public int TrainingsHours { get; set; }

        public int ConsultationsHours { get; set; }

        public int SelfWorkHours { get; set; }

        public Guid SelectiveBlockId { get; set; }

        public Guid FinalControlTypeId { get; set; }

        public Guid EducationalProgramId { get; set; }

        public Guid? WorkingProgramId { get; set; }

        public SelectiveBlock SelectiveBlock { get; set; }

        public FinalControlType FinalControlType { get; set; }

        public EducationalProgram EducationalProgram { get; set; }

        public ICollection<SubjectCompetence> SubjectCompetences { get; set; }

        public ICollection<SubjectProgramResult> SubjectProgramResults { get; set; }
    }
}