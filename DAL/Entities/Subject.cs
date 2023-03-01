namespace DAL.Entities
{
    public class Subject : BaseEntity
    {
        public string? Name { get; set; }

        public int Credits { get; set; }

        public int Semester { get; set; }

        public int LecturesHours { get; set; }

        public int SeminarsHours { get; set; }

        public int SelfWorkHours { get; set; }

        public Guid SelectiveBlockId { get; set; }

        public Guid FinalControlTypeId { get; set; }

        public SelectiveBlock? SelectiveBlock { get; set; }

        public FinalControlType? FinalControlType { get; set; }

        public ICollection<SubjectCompetence>? SubjectCompetences { get; set; }

        public ICollection<SubjectProgramResult>? SubjectProgramResults { get; set; }
    }
}