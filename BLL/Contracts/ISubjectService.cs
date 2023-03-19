using BLL.Models;
using BLL.Results;

namespace BLL.Contracts
{
    public interface ISubjectService
    {
        Task<Result<IEnumerable<SubjectModel>>> GetAllAsync();

        Task<Result<SubjectGetModel>> GetByIdAsync(Guid id);

        Task<Result<Guid>> CreateAsync(SubjectCreateModel model);

        Task<Result> UpdateAsync(SubjectUpdateModel model);
    }
}