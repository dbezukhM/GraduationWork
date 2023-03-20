using BLL.Models;
using BLL.Results;

namespace BLL.Contracts
{
    public interface ILookupService
    {
        Task<Result<IEnumerable<IdNameModel<Guid>>>> GetAreaOfExpertiseAsync();

        Task<Result<IEnumerable<IdNameModel<Guid>>>> GetSpecializationsAsync(Guid areaOfExpertiseId);

        Task<Result<IEnumerable<IdNameModel<Guid>>>> GetUniversitiesAsync();

        Task<Result<IEnumerable<IdNameModel<Guid>>>> GetFacultiesAsync(Guid universityId);

        Task<Result<IEnumerable<IdNameModel<Guid>>>> GetEducationalProgramsTypesAsync();

        Task<Result<IEnumerable<IdNameModel<Guid>>>> GetEducationalProgramsAsync();

        Task<Result<IEnumerable<CompetenceModel>>> GetCompetencesAsync(Guid educationalProgramId);

        Task<Result<IEnumerable<ProgramResultModel>>> GetProgramResultsAsync(Guid educationalProgramId);

        Task<Result<IEnumerable<IdNameModel<Guid>>>> GetFinalControlTypesAsync();

        Task<Result<IEnumerable<IdNameModel<Guid>>>> GetSelectiveBlocksAsync();
    }
}