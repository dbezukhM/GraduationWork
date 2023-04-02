using AutoMapper;
using BLL.Contracts;
using BLL.Errors;
using BLL.Models;
using BLL.Results;
using DAL.Contracts;
using DAL.Entities;

namespace BLL.Services;

public class SubjectService : ISubjectService
{
    private readonly IEpRepositoryAsync<Subject> _subjectRepository;
    private readonly IEpRepositoryAsync<Competence> _competenceRepository;
    private readonly IEpRepositoryAsync<SubjectCompetence> _subjectCompetenceRepository;
    private readonly IEpRepositoryAsync<ProgramResult> _programResultRepository;
    private readonly IEpRepositoryAsync<SubjectProgramResult> _subjectProgramResultRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SubjectService(
        IEpRepositoryAsync<Subject> subjectRepository,
        IEpRepositoryAsync<Competence> competenceRepository,
        IEpRepositoryAsync<SubjectCompetence> subjectCompetenceRepository,
        IEpRepositoryAsync<ProgramResult> programResultRepository,
        IEpRepositoryAsync<SubjectProgramResult> subjectProgramResultRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _competenceRepository = competenceRepository;
        _subjectCompetenceRepository = subjectCompetenceRepository;
        _programResultRepository = programResultRepository;
        _subjectProgramResultRepository = subjectProgramResultRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _subjectCompetenceRepository = subjectCompetenceRepository;
    }

    public async Task<Result<IEnumerable<SubjectModel>>> GetAllAsync()
    {
        var subjects = await _subjectRepository.GetAllAsync(
            s => s.EducationalProgram,
            s => s.SelectiveBlock);
        var result = _mapper.Map<IEnumerable<SubjectModel>>(subjects);

        return Result.Success(result);
    }

    public async Task<Result<SubjectGetModel>> GetByIdAsync(Guid id)
    {
        var subject = await _subjectRepository.GetWithDetailsAsync(id,
            s => s.SelectiveBlock,
            s => s.FinalControlType,
            s => s.EducationalProgram,
            s => s.EducationalProgram.EducationalProgramsType,
            s => s.SubjectCompetences,
            s => s.SubjectProgramResults);
        if (subject == null)
        {
            return Result.NotFound<SubjectGetModel>(BlErrors.NotFound(id));
        }

        var competencesIds = subject.SubjectCompetences.Select(c => c.CompetenceId);
        var programResultsIds = subject.SubjectProgramResults.Select(r => r.ProgramResultId);
        var competences = await _competenceRepository.FindAllAsync(x => competencesIds.Contains(x.Id));
        var programResults = await _programResultRepository.FindAllAsync(x => programResultsIds.Contains(x.Id));

        var result = _mapper.Map<SubjectGetModel>(subject);
        result.Competences = _mapper.Map<IEnumerable<CompetenceModel>>(competences);
        result.ProgramResults = _mapper.Map<IEnumerable<ProgramResultModel>>(programResults);

        return Result.Success(result);
    }

    public async Task<Result<Guid>> CreateAsync(SubjectCreateModel model)
    {
        var isNameExisting = await _subjectRepository.ExistsAsync(x =>
            x.Name == model.Name && x.EducationalProgramId == model.EducationalProgramId);
        if (isNameExisting)
        {
            return Result.ValidationError<Guid>(BlErrors.EducationalProgramNameNotUnique);
        }

        var entity = _mapper.Map<Subject>(model);

        return await _unitOfWork.NewTransaction(async () =>
        {
            var createdEntity = await _subjectRepository.AddAsync(entity);

            return Result.Success(createdEntity.Id);
        });
    }

    public async Task<Result> UpdateAsync(SubjectUpdateModel model)
    {
        var existingEntity = await _subjectRepository.GetWithDetailsAsync(model.Id,
            s => s.SubjectCompetences,
            s => s.SubjectProgramResults);
        if (existingEntity == null)
        {
            return Result.NotFound(BlErrors.NotFound(model.Id));
        }

        var isNameExisting = await _subjectRepository.ExistsAsync(x => x.Id != model.Id && x.Name == model.Name);
        if (isNameExisting)
        {
            return Result.ValidationError(BlErrors.SubjectNameNotUnique);
        }

        var subjectCompetenceModels = model.CompetencesIds.Select(id => new SubjectCompetence
        {
            SubjectId = model.Id,
            CompetenceId = id,
        });
        var subjectCompetenceToDelete = existingEntity.SubjectCompetences
            .Where(x => !model.CompetencesIds.Contains(x.CompetenceId));
        var subjectProgramResultModels = model.ProgramResultsIds.Select(id => new SubjectProgramResult
        {
            SubjectId = model.Id,
            ProgramResultId = id,
        });
        var subjectProgramResultToDelete = existingEntity.SubjectProgramResults
            .Where(x => !model.ProgramResultsIds.Contains(x.ProgramResultId));

        return await _unitOfWork.NewTransaction(() =>
        {
            existingEntity.Name = model.Name;
            existingEntity.Credits = model.Credits;
            existingEntity.Semester = model.Semester;
            existingEntity.LecturesHours = model.LecturesHours;
            existingEntity.SeminarsHours = model.SeminarsHours;
            existingEntity.PracticalClassesHours = model.PracticalClassesHours;
            existingEntity.LaboratoryClassesHours = model.LaboratoryClassesHours;
            existingEntity.TrainingsHours = model.TrainingsHours;
            existingEntity.ConsultationsHours = model.ConsultationsHours;
            existingEntity.SelfWorkHours = model.SelfWorkHours;
            existingEntity.SelectiveBlockId = model.SelectiveBlockId;
            existingEntity.FinalControlTypeId = model.FinalControlTypeId;

            _subjectCompetenceRepository.Delete(subjectCompetenceToDelete);
            _subjectProgramResultRepository.Delete(subjectProgramResultToDelete);
            existingEntity.SubjectCompetences = subjectCompetenceModels.ToList();
            existingEntity.SubjectProgramResults = subjectProgramResultModels.ToList();

            return Result.SuccessTask();
        });
    }
}