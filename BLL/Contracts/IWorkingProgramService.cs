using BLL.Results;
using BLL.Models;

namespace BLL.Contracts
{
    public interface IWorkingProgramService
    {
        Task<Result<Guid>> CreateAsync(WorkingProgramCreateModel model);

        Task<Result<WorkingProgramModel>> GetWorkingProgramFileAsync(Guid workingProgramId);

        Task<Result<IEnumerable<WorkingProgramGetModel>>> GetAllAsync();

        Task<Result<WorkingProgramDetailsModel>> GetByIdAsync(Guid id);

        Task<Result> ApproveWorkingProgramAsync(Guid id, string approverEmail);

        Task<Result<Guid>> CreateCommentAsync(CommentCreateModel model);

        Task<Result> DeleteByIdAsync(Guid id);

        Task<Result> RejectAsync(RejectModel model, string email);
    }
}