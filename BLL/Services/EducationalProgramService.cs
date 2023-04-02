using AutoMapper;
using BLL.Contracts;
using BLL.Errors;
using BLL.Models;
using BLL.Results;
using DAL.Contracts;
using DAL.Entities;

namespace BLL.Services
{
    public class EducationalProgramService : IEducationalProgramService
    {
        private readonly IEpRepositoryAsync<EducationalProgram> _educationalProgramRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EducationalProgramService(
            IEpRepositoryAsync<EducationalProgram> educationalProgramRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _educationalProgramRepository = educationalProgramRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<EducationalProgramModel>>> GetAllAsync()
        {
            var educationalPrograms = await _educationalProgramRepository.GetAllAsync(
                e => e.Faculty,
                e => e.Faculty.University,
                e => e.EducationalProgramsType,
                e => e.Specialization);
            var result = _mapper.Map<IEnumerable<EducationalProgramModel>>(educationalPrograms);

            return Result.Success(result);
        }

        public async Task<Result<EducationalProgramGetModel>> GetByIdAsync(Guid id)
        {
            var educationalProgram = await _educationalProgramRepository.GetWithDetailsAsync(id,
                e => e.Competences,
                e => e.ProgramResults,
                e => e.Faculty,
                e => e.Faculty.University,
                e => e.EducationalProgramsType,
                e => e.Subjects,
                e => e.Specialization,
                e => e.Specialization.AreaOfExpertise);

            var result = _mapper.Map<EducationalProgramGetModel>(educationalProgram);

            return Result.Success(result);
        }

        public async Task<Result<Guid>> CreateAsync(EducationalProgramCreateModel model)
        {
            var isNameExisting = await _educationalProgramRepository.ExistsAsync(x =>
                x.Name == model.Name && x.FacultyId == model.FacultyId &&
                x.SpecializationId == model.SpecializationId &&
                x.EducationalProgramsTypeId == model.EducationalProgramsTypeId);
            if (isNameExisting)
            {
                return Result.ValidationError<Guid>(BlErrors.EducationalProgramNameNotUnique);
            }

            var entity = _mapper.Map<EducationalProgram>(model);

            return await _unitOfWork.NewTransaction(async () =>
            {
                var createdEntity = await _educationalProgramRepository.AddAsync(entity);

                return Result.Success(createdEntity.Id);
            });
        }

        public async Task<Result> UpdateAsync(EducationalProgramUpdateModel model)
        {
            var existingEntity = await _educationalProgramRepository.FindAsync(x => x.Id == model.Id);
            if (existingEntity == null)
            {
                return Result.NotFound(BlErrors.NotFound(model.Id));
            }

            var isNameExisting = await _educationalProgramRepository.ExistsAsync(x => x.Id != model.Id &&
                x.Name == model.Name && x.FacultyId == model.FacultyId &&
                x.SpecializationId == model.SpecializationId &&
                x.EducationalProgramsTypeId == model.EducationalProgramsTypeId);
            if (isNameExisting)
            {
                return Result.ValidationError(BlErrors.EducationalProgramNameNotUnique);
            }

            return await _unitOfWork.NewTransaction(() =>
            {
                existingEntity.Name = model.Name;
                existingEntity.FacultyId = model.FacultyId;
                existingEntity.SpecializationId = model.SpecializationId;
                existingEntity.EducationalProgramsTypeId = model.EducationalProgramsTypeId;

                return Result.SuccessTask();
            });
        }
    }
}