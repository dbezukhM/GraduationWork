using AutoMapper;
using BLL.Contracts;
using BLL.Errors;
using BLL.Models;
using BLL.Results;
using DAL.Contracts;
using DAL.Entities;

namespace BLL.Services
{
    public class CompetenceService : ICompetenceService
    {
        private readonly IEpRepositoryAsync<Competence> _competenceRepository;
        private readonly IEpRepositoryAsync<Subject> _subjectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompetenceService(
            IEpRepositoryAsync<Competence> competenceRepository,
            IEpRepositoryAsync<Subject> subjectRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _competenceRepository = competenceRepository;
            _subjectRepository = subjectRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CompetenceGetModel>>> GetAllAsync()
        {
            var competences = await _competenceRepository.GetAllAsync(
                c => c.EducationalProgram,
                c => c.SubjectCompetences);
            var subjects = await _subjectRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<CompetenceGetModel>>(competences);
            foreach (var item in result)
            {
                var subjectIds = competences.First(x => x.Id == item.Id).SubjectCompetences
                    .Select(x => x.SubjectId);
                item.Subjects =
                    _mapper.Map<IEnumerable<IdNameModel<Guid>>>(subjects.Where(x => subjectIds.Contains(x.Id)));
            }

            return Result.Success(result);
        }

        public async Task<Result<Guid>> CreateAsync(CompetenceCreateModel model)
        {
            var isNameExisting = await _competenceRepository.ExistsAsync(x => x.Name == model.Name);
            if (isNameExisting)
            {
                return Result.ValidationError<Guid>(BlErrors.CompetenceNameNotUnique);
            }

            var entity = _mapper.Map<Competence>(model);

            return await _unitOfWork.NewTransaction(async () =>
            {
                var createdEntity = await _competenceRepository.AddAsync(entity);

                return Result.Success(createdEntity.Id);
            });
        }

        public async Task<Result> UpdateAsync(CompetenceUpdateModel model)
        {
            var existingEntity = await _competenceRepository.FindAsync(x => x.Id == model.Id);
            if (existingEntity == null)
            {
                return Result.NotFound(BlErrors.NotFound(model.Id));
            }

            var isNameExisting = await _competenceRepository.ExistsAsync(x => x.Id != model.Id && x.Name == model.Name);
            if (isNameExisting)
            {
                return Result.ValidationError<Guid>(BlErrors.CompetenceNameNotUnique);
            }

            return await _unitOfWork.NewTransaction(() =>
            {
                existingEntity.Name = model.Name;
                existingEntity.Description = model.Description;
                existingEntity.EducationalProgramId = model.EducationalProgramId;

                return Result.SuccessTask();
            });
        }

        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            var isExisting = await _competenceRepository.ExistsAsync(x => x.Id == id);
            if (!isExisting)
            {
                return Result.NotFound(BlErrors.NotFound(id));
            }

            return await _unitOfWork.NewTransaction(async () =>
            {
                await _competenceRepository.DeleteAsync(id);
                return Result.Success();
            });
        }
    }
}