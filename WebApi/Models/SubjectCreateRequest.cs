namespace WebApi.Models
{
    public class SubjectCreateRequest : IRequest
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
    }
}