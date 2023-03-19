using BLL.Models;
using BLL.Results;

namespace BLL.Contracts
{
    public interface ICompetenceService
    {
        Task<Result<IEnumerable<CompetenceGetModel>>> GetAllAsync();

        Task<Result<Guid>> CreateAsync(CompetenceCreateModel model);

        Task<Result> UpdateAsync(CompetenceUpdateModel model);

        Task<Result> DeleteByIdAsync(Guid id);
    }
}