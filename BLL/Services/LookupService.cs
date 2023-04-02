using AutoMapper;
using BLL.Contracts;
using BLL.Models;
using BLL.Results;
using DAL.Contracts;
using DAL.Entities;

namespace BLL.Services
{
    public class LookupService : ILookupService
    {
        private readonly IEpRepositoryAsync<SelectiveBlock> _selectiveBlockRepository;
        private readonly IEpRepositoryAsync<FinalControlType> _finalControlTypeRepository;
        private readonly IEpRepositoryAsync<ProgramResult> _programResultRepository;
        private readonly IEpRepositoryAsync<Competence> _competenceRepository;
        private readonly IEpRepositoryAsync<EducationalProgram> _educationalProgramRepository;
        private readonly IEpRepositoryAsync<EducationalProgramsType> _educationalProgramsTypeRepository;
        private readonly IEpRepositoryAsync<Faculty> _facultyRepository;
        private readonly IEpRepositoryAsync<University> _universityRepository;
        private readonly IEpRepositoryAsync<Specialization> _specializationRepository;
        private readonly IEpRepositoryAsync<AreaOfExpertise> _areaOfExpertiseRepository;
        private readonly IEpRepositoryAsync<Subject> _subjectRepository;
        private readonly IMapper _mapper;

        public LookupService(
            IEpRepositoryAsync<SelectiveBlock> selectiveBlockRepository,
            IEpRepositoryAsync<FinalControlType> finalControlTypeRepository,
            IEpRepositoryAsync<ProgramResult> programResultRepository,
            IEpRepositoryAsync<Competence> competenceRepository,
            IEpRepositoryAsync<EducationalProgram> educationalProgramRepository,
            IEpRepositoryAsync<EducationalProgramsType> educationalProgramsTypeRepository,
            IEpRepositoryAsync<Faculty> facultyRepository,
            IEpRepositoryAsync<University> universityRepository,
            IEpRepositoryAsync<Specialization> specializationRepository,
            IEpRepositoryAsync<AreaOfExpertise> areaOfExpertiseRepository,
            IEpRepositoryAsync<Subject> subjectRepository,
            IMapper mapper)
        {
            _selectiveBlockRepository = selectiveBlockRepository;
            _finalControlTypeRepository = finalControlTypeRepository;
            _programResultRepository = programResultRepository;
            _competenceRepository = competenceRepository;
            _educationalProgramRepository = educationalProgramRepository;
            _educationalProgramsTypeRepository = educationalProgramsTypeRepository;
            _facultyRepository = facultyRepository;
            _universityRepository = universityRepository;
            _specializationRepository = specializationRepository;
            _areaOfExpertiseRepository = areaOfExpertiseRepository;
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<IdNameModel<Guid>>>> GetAreaOfExpertiseAsync()
        {
            var result = await _areaOfExpertiseRepository.GetAllAsync();

            return Result.Success(_mapper.Map<IEnumerable<IdNameModel<Guid>>>(result));
        }

        public async Task<Result<IEnumerable<IdNameModel<Guid>>>> GetSpecializationsAsync(Guid areaOfExpertiseId)
        {
            var result = await _specializationRepository.FindAllAsync(x => x.AreaOfExpertiseId == areaOfExpertiseId);

            return Result.Success(_mapper.Map<IEnumerable<IdNameModel<Guid>>>(result));
        }

        public async Task<Result<IEnumerable<IdNameModel<Guid>>>> GetUniversitiesAsync()
        {
            var result = await _universityRepository.GetAllAsync();

            return Result.Success(_mapper.Map<IEnumerable<IdNameModel<Guid>>>(result));
        }

        public async Task<Result<IEnumerable<IdNameModel<Guid>>>> GetFacultiesAsync(Guid universityId)
        {
            var result = await _facultyRepository.FindAllAsync(x => x.UniversityId == universityId);

            return Result.Success(_mapper.Map<IEnumerable<IdNameModel<Guid>>>(result));
        }

        public async Task<Result<IEnumerable<IdNameModel<Guid>>>> GetEducationalProgramsTypesAsync()
        {
            var result = await _educationalProgramsTypeRepository.GetAllAsync();

            return Result.Success(_mapper.Map<IEnumerable<IdNameModel<Guid>>>(result));
        }

        public async Task<Result<IEnumerable<IdNameModel<Guid>>>> GetEducationalProgramsAsync()
        {
            var result = await _educationalProgramRepository.GetAllAsync(x => x.EducationalProgramsType);

            return Result.Success(_mapper.Map<IEnumerable<IdNameModel<Guid>>>(result));
        }

        public async Task<Result<IEnumerable<CompetenceModel>>> GetCompetencesAsync(Guid educationalProgramId)
        {
            var result = await _competenceRepository.FindAllAsync(x => x.EducationalProgramId == educationalProgramId);

            return Result.Success(_mapper.Map<IEnumerable<CompetenceModel>>(result));
        }

        public async Task<Result<IEnumerable<ProgramResultModel>>> GetProgramResultsAsync(Guid educationalProgramId)
        {
            var result =
                await _programResultRepository.FindAllAsync(x => x.EducationalProgramId == educationalProgramId);

            return Result.Success(_mapper.Map<IEnumerable<ProgramResultModel>>(result));
        }

        public async Task<Result<IEnumerable<IdNameModel<Guid>>>> GetFinalControlTypesAsync()
        {
            var result = await _finalControlTypeRepository.GetAllAsync();

            return Result.Success(_mapper.Map<IEnumerable<IdNameModel<Guid>>>(result));
        }

        public async Task<Result<IEnumerable<IdNameModel<Guid>>>> GetSelectiveBlocksAsync()
        {
            var result = await _selectiveBlockRepository.GetAllAsync();

            return Result.Success(_mapper.Map<IEnumerable<IdNameModel<Guid>>>(result));
        }

        public async Task<Result<IEnumerable<IdNameModel<Guid>>>> GetSubjectsAsync()
        {
            var result = await _subjectRepository.GetAllAsync();

            return Result.Success(_mapper.Map<IEnumerable<IdNameModel<Guid>>>(result));
        }
    }
}