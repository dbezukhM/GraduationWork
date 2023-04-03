using AutoMapper;
using BLL.Contracts;
using BLL.Errors;
using BLL.Models;
using BLL.Results;
using DAL.Contracts;
using DAL.Entities;

namespace BLL.Services
{
    public class ProgramResultService : IProgramResultService
    {
        private readonly IEpRepositoryAsync<ProgramResult> _programResultRepository;
        private readonly IEpRepositoryAsync<Subject> _subjectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProgramResultService(
            IEpRepositoryAsync<ProgramResult> programResultRepository,
            IEpRepositoryAsync<Subject> subjectRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _programResultRepository = programResultRepository;
            _subjectRepository = subjectRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ProgramResultGetModel>>> GetAllAsync()
        {
            var programResults = await _programResultRepository.GetAllAsync(
                r => r.EducationalProgram,
                r => r.EducationalProgram.EducationalProgramsType,
                r => r.SubjectProgramResults);
            var subjects = await _subjectRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<ProgramResultGetModel>>(programResults);
            foreach (var item in result)
            {
                var subjectIds = programResults.First(x => x.Id == item.Id).SubjectProgramResults
                    .Select(x => x.SubjectId);
                item.Subjects =
                    _mapper.Map<IEnumerable<IdNameModel<Guid>>>(subjects.Where(x => subjectIds.Contains(x.Id)));
            }

            return Result.Success(result);
        }

        public async Task<Result<Guid>> CreateAsync(ProgramResultCreateModel model)
        {
            var isNameExisting = await _programResultRepository.ExistsAsync(x => x.Name == model.Name);
            if (isNameExisting)
            {
                return Result.ValidationError<Guid>(BlErrors.ProgramResultNameNotUnique);
            }

            var entity = _mapper.Map<ProgramResult>(model);

            return await _unitOfWork.NewTransaction(async () =>
            {
                var createdEntity = await _programResultRepository.AddAsync(entity);

                return Result.Success(createdEntity.Id);
            });
        }

        public async Task<Result> UpdateAsync(ProgramResultUpdateModel model)
        {
            var existingEntity = await _programResultRepository.FindAsync(x => x.Id == model.Id);
            if (existingEntity == null)
            {
                return Result.NotFound(BlErrors.NotFound(model.Id));
            }

            var isNameExisting = await _programResultRepository.ExistsAsync(x => x.Id != model.Id && x.Name == model.Name);
            if (isNameExisting)
            {
                return Result.ValidationError<Guid>(BlErrors.ProgramResultNameNotUnique);
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
            var isExisting = await _programResultRepository.ExistsAsync(x => x.Id == id);
            if (!isExisting)
            {
                return Result.NotFound(BlErrors.NotFound(id));
            }

            return await _unitOfWork.NewTransaction(async () =>
            {
                await _programResultRepository.DeleteAsync(id);
                return Result.Success();
            });
        }
    }
}