using BLL.Models;
using BLL.Results;

namespace BLL.Contracts
{
    public interface IEducationalProgramService
    {
        Task<Result<IEnumerable<EducationalProgramModel>>> GetAllAsync();

        Task<Result<EducationalProgramGetModel>> GetByIdAsync(Guid id);

        Task<Result<Guid>> CreateAsync(EducationalProgramCreateModel model);

        Task<Result> UpdateAsync(EducationalProgramUpdateModel model);
    }
}