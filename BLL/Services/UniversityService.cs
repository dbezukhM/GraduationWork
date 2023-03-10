using AutoMapper;
using BLL.Contracts;
using BLL.Errors;
using BLL.Models;
using BLL.Results;
using DAL.Contracts;
using DAL.Entities;

namespace BLL.Services
{
    public class UniversityService : IUniversityService
    {
        private readonly IEpRepositoryAsync<University> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UniversityService(
            IEpRepositoryAsync<University> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<UniversityModel>>> GetAllAsync()
        {
            var universities = await _repository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<UniversityModel>>(universities);

            return Result.Success(result);
        }

        public async Task<Result<UniversityModel>> GetByIdAsync(Guid id)
        {
            var university = await _repository.GetAsync(id);
            var result = _mapper.Map<UniversityModel>(university);

            return Result.Success(result);
        }

        public async Task<Result<Guid>> CreateAsync(UniversityModel model)
        {
            var isExisting = await _repository.ExistsAsync(x => x.Name == model.Name);
            if (isExisting)
            {
                return Result.ValidationError<Guid>(BlErrors.ExistingEntity);
            }

            var entity = _mapper.Map<University>(model);

            return await _unitOfWork.NewTransaction(async () =>
            {
                var createdEntity = await _repository.AddAsync(entity);

                return Result.Success(createdEntity.Id);
            });
        }

        public async Task<Result> UpdateAsync(UniversityModel model)
        {
            var existingEntity = await _repository.FindAsync(x => x.Id == model.Id);
            if (existingEntity == null)
            {
                return Result.NotFound(BlErrors.NotFound(model.Id));
            }

            return await _unitOfWork.NewTransaction(() =>
            {
                existingEntity.Name = model.Name;
                return Result.SuccessTask();
            });
        }

        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            var isExisting = await _repository.ExistsAsync(x => x.Id == id);
            if (!isExisting)
            {
                return Result.NotFound(BlErrors.NotFound(id));
            }

            return await _unitOfWork.NewTransaction(async () =>
            {
                await _repository.DeleteAsync(id);
                return Result.Success();
            });
        }
    }
}