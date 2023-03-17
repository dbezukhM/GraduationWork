using System.Collections;
using BLL.Contracts;
using BLL.Errors;
using BLL.Extensions;
using BLL.Models;
using BLL.Results;
using BLL.Settings;
using DAL.Contracts;
using DAL.DatabaseInitializers;
using DAL.Entities;
using Microsoft.Extensions.Options;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace BLL.Services
{
    public class FileGenerator : IFileGenerator
    {
        private readonly ProgramSettings _programSettings;
        private readonly IEpRepositoryAsync<Subject> _subjectRepository;
        private readonly IEpRepositoryAsync<SelectiveBlock> _selectiveBlockRepository;
        private readonly IEpRepositoryAsync<FinalControlType> _finalControlTypeRepository;
        private readonly IEpRepositoryAsync<SubjectProgramResult> _subjectProgramResultRepository;
        private readonly IEpRepositoryAsync<ProgramResult> _programResultRepository;
        private readonly IEpRepositoryAsync<SubjectCompetence> _subjectCompetenceRepository;
        private readonly IEpRepositoryAsync<Competence> _competenceRepository;
        private readonly IEpRepositoryAsync<EducationalProgram> _educationalProgramRepository;
        private readonly IEpRepositoryAsync<EducationalProgramsType> _educationalProgramsTypeRepository;
        private readonly IEpRepositoryAsync<Faculty> _facultyRepository;
        private readonly IEpRepositoryAsync<University> _universityRepository;
        private readonly IEpRepositoryAsync<Specialization> _specializationRepository;
        private readonly IEpRepositoryAsync<AreaOfExpertise> _areaOfExpertiseRepository;
        private readonly IFileProvider _fileProvider;

        private static string CompetenceTag = "<COMPETENCES>";
        private static string CompetenceTagValue = "Компетентності, на досягнення яких спрямована дана дисципліна:";

        public FileGenerator(
            IOptionsSnapshot<ProgramSettings> programSettings,
            IEpRepositoryAsync<Subject> subjectRepository,
            IEpRepositoryAsync<SelectiveBlock> selectiveBlockRepository,
            IEpRepositoryAsync<FinalControlType> finalControlTypeRepository,
            IEpRepositoryAsync<SubjectProgramResult> subjectProgramResultRepository,
            IEpRepositoryAsync<ProgramResult> programResultRepository,
            IEpRepositoryAsync<SubjectCompetence> subjectCompetenceRepository,
            IEpRepositoryAsync<Competence> competenceRepository,
            IEpRepositoryAsync<EducationalProgram> educationalProgramRepository,
            IEpRepositoryAsync<EducationalProgramsType> educationalProgramsTypeRepository,
            IEpRepositoryAsync<Faculty> facultyRepository,
            IEpRepositoryAsync<University> universityRepository,
            IEpRepositoryAsync<Specialization> specializationRepository,
            IEpRepositoryAsync<AreaOfExpertise> areaOfExpertiseRepository,
            IFileProvider fileProvider)
        {
            _programSettings = programSettings.Value;
            _subjectRepository = subjectRepository;
            _selectiveBlockRepository = selectiveBlockRepository;
            _finalControlTypeRepository = finalControlTypeRepository;
            _subjectProgramResultRepository = subjectProgramResultRepository;
            _programResultRepository = programResultRepository;
            _subjectCompetenceRepository = subjectCompetenceRepository;
            _competenceRepository = competenceRepository;
            _educationalProgramRepository = educationalProgramRepository;
            _educationalProgramsTypeRepository = educationalProgramsTypeRepository;
            _facultyRepository = facultyRepository;
            _universityRepository = universityRepository;
            _specializationRepository = specializationRepository;
            _areaOfExpertiseRepository = areaOfExpertiseRepository;
            _fileProvider = fileProvider;
        }

        public async Task<Result<MemoryStream>> GenerateFile(Guid subjectId)
        {
            var subjectDetailsModel = await GetDetailsModel(subjectId);
            if (subjectDetailsModel == null)
            {
                return Result.NotFound<MemoryStream>(BlErrors.NotFound(subjectId));
            }

            var file = await _fileProvider.GetFileAsync(_programSettings.TemplateFileName);
            using var docx = DocX.Load(new MemoryStream(file.Value.Contents));

            var dictionary = GetDictionary(subjectDetailsModel);
            foreach (var item in dictionary)
            {
                docx.ReplaceText(item.Key, item.Value);
            }

            var format = new Formatting
            {
                Size = 12,
            };

            var bulletList = docx.AddList(null, 0, ListItemType.Bulleted);
            foreach (var item in subjectDetailsModel!.Competences)
            {
                docx.AddListItem(bulletList, $"{item.Description}.", 0, formatting: format);
            }

            var paragraph = docx.Paragraphs.FirstOrDefault(p => p.Text.Contains("<COMPETENCES>"));
            paragraph?.InsertListAfterSelf(bulletList);
            docx.ReplaceText(CompetenceTag, CompetenceTagValue);

            foreach (var programResult in subjectDetailsModel.ProgramResults)
            {
                docx.Tables[1].InsertRow();
                docx.Tables[1].Rows.Last().Cells[0].Paragraphs[0]
                    .Append($"{programResult.Name}. {programResult.Description}", format);
            }

            docx.Tables[1].Design = TableDesign.TableGrid;

            LeaveNecessaryGrades(docx, subjectDetailsModel.FinalControlType);

            var stream = new MemoryStream();
            docx.SaveAs(stream);
            stream.Flush();

            return Result.Success(stream);
        }

        private async Task<SubjectDetailsModel> GetDetailsModel(Guid subjectId)
        {
            var subject = await _subjectRepository.GetAsync(subjectId);
            if (subject == null)
            {
                return null;
            }

            var selectiveBlock = await _selectiveBlockRepository.GetAsync(subject.SelectiveBlockId);
            var finalControlType = await _finalControlTypeRepository.GetAsync(subject.FinalControlTypeId);
            var programResultIds = (await _subjectProgramResultRepository.FindAllAsync(r => r.SubjectId == subject.Id))
                .Select(r => r.ProgramResultId);
            var programResults = await _programResultRepository.FindAllAsync(r => programResultIds.Contains(r.Id));
            var competenceIds = (await _subjectCompetenceRepository.FindAllAsync(c => c.SubjectId == subject.Id))
                .Select(c => c.CompetenceId);
            var competences = await _competenceRepository.FindAllAsync(c => competenceIds.Contains(c.Id));
            var educationalProgram = await _educationalProgramRepository.GetAsync(subject.EducationalProgramId);
            var educationalProgramType =
                await _educationalProgramsTypeRepository.GetAsync(educationalProgram.EducationalProgramsTypeId);
            var faculty = await _facultyRepository.GetAsync(educationalProgram.FacultyId);
            var university = await _universityRepository.GetAsync(faculty.UniversityId);
            var specialization = await _specializationRepository.GetAsync(educationalProgram.SpecializationId);
            var areaOfExpertise = await _areaOfExpertiseRepository.GetAsync(specialization.AreaOfExpertiseId);

            var result = new SubjectDetailsModel
            {
                Subject = subject,
                University = university,
                Faculty = faculty,
                AreaOfExpertise = areaOfExpertise,
                Specialization = specialization,
                EducationalProgramsType = educationalProgramType,
                EducationalProgram = educationalProgram,
                SelectiveBlock = selectiveBlock,
                FinalControlType = finalControlType,
                Competences = competences,
                ProgramResults = programResults,
            };

            return result;
        }

        private Dictionary<string, string> GetDictionary(SubjectDetailsModel model)
        {
            var lecturesHours = model.Subject.LecturesHours;
            var seminarsHours = model.Subject.SeminarsHours;
            var practicalClassesHours = model.Subject.PracticalClassesHours;
            var laboratoryClassesHours = model.Subject.LaboratoryClassesHours;
            var trainingsHours = model.Subject.TrainingsHours;
            var consultationsHours = model.Subject.ConsultationsHours;
            var selfWorkHours = model.Subject.SelfWorkHours;
            var sumHours = lecturesHours + seminarsHours + practicalClassesHours + laboratoryClassesHours +
                           trainingsHours + consultationsHours + selfWorkHours;
            var result = new Dictionary<string, string>
            {
                { "<UNIVERSITYNAME>", model.University.Name.ToUpper() },
                { "<FACULTY>", model.Faculty.Name.ToUpper() },
                { "<SUBJECTNAME>", model.Subject.Name.ToUpper() },
                { "<AREAOFEXPERTISE>", $"{model.AreaOfExpertise.Number} «{model.AreaOfExpertise.Name.FirstCharToUpper()}»" },
                { "<SPECIALIZATION>", $"{model.Specialization.Number} «{model.Specialization.Name.FirstCharToUpper()}»" },
                { "<EDUCATIONALPROGRAMTYPE>", model.EducationalProgramsType.Name.ToLower() },
                { "<EDUCATIONALPROGRAM>", $"«{model.EducationalProgram.Name.FirstCharToUpper()}»" },
                { "<SELECTIVEBLOCK>", model.SelectiveBlock.Name.ToLower() },
                { "<YEARNOW>", DateTime.Now.ToString("yyyy") },
                { "<YEARTO>", DateTime.Now.AddYears(1).ToString("yyyy") },
                { "<SUBJECTSEMESTER>", model.Subject.Semester.ToString() },
                { "<SUBJECTCREDITS>", model.Subject.Credits.ToString() },
                { "<FINALCONTROLTYPE>", model.FinalControlType.Name.ToLower() },
                { "<LECTURESHOURS>", lecturesHours != 0 ? lecturesHours.ToString() : "-" },
                { "<SEMINARSHOURS>", seminarsHours != 0 ? seminarsHours.ToString() : "-" },
                { "<PRACTICALCLASSESHOURS>", practicalClassesHours != 0 ? practicalClassesHours.ToString() : "-" },
                { "<LABORATORYCLASSESHOURS>", laboratoryClassesHours != 0 ? laboratoryClassesHours.ToString() : "-" },
                { "<TRAININGSHOURS>", trainingsHours != 0 ? trainingsHours.ToString() : "-" },
                { "<CONSULTATIONSHOURS>", consultationsHours != 0 ? consultationsHours.ToString() : "-" },
                { "<SELFWORKHOURS>", selfWorkHours != 0 ? selfWorkHours.ToString() : "-" },
                { "<SUMHOURS> ", sumHours.ToString() },
            };

            return result;
        }

        private DocX LeaveNecessaryGrades(DocX docx, FinalControlType type)
        {
            if (type.Id == DatabaseSeeder.FinalControlTypeExam)
            {
                var rowsCount = docx.Tables[2].RowCount;
                docx.Tables[2].RemoveRow(rowsCount - 1);
                docx.Tables[2].RemoveRow(rowsCount - 2);
            }
            else if (type.Id == DatabaseSeeder.FinalControlTypeCredit)
            {
                foreach (var i in Enumerable.Range(0, 4))
                {
                    docx.Tables[2].RemoveRow(0);
                }
            }

            return docx;
        }
    }
}