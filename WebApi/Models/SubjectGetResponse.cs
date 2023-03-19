using BLL.Models;

namespace WebApi.Models
{
    public class SubjectGetResponse : IResponse
    {
        public Guid Id { get; set; }

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

        public IdNameModel<Guid> SelectiveBlock { get; set; }

        public IdNameModel<Guid> FinalControlType { get; set; }

        public IdNameModel<Guid> EducationalProgram { get; set; }

        public IEnumerable<CompetenceModel> Competences { get; set; }

        public IEnumerable<ProgramResultModel> ProgramResults { get; set; }
    }
}