using AutoMapper;
using BLL.Contracts;
using BLL.Errors;
using BLL.Models;
using BLL.Results;
using DAL.Contracts;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Runtime;

namespace BLL.Services
{
    public class WorkingProgramService : IWorkingProgramService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEpRepositoryAsync<Subject> _subjectRepository;
        private readonly IWpRepositoryAsync<WorkingProgram> _workingProgramRepository;
        private readonly IWpRepositoryAsync<Comment> _commentRepository;
        private readonly IFileProvider _fileProvider;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<Person> _userManager;

        public WorkingProgramService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IEpRepositoryAsync<Subject> subjectRepository,
            IWpRepositoryAsync<WorkingProgram> workingProgramRepository,
            IWpRepositoryAsync<Comment> commentRepository,
            IFileProvider fileProvider,
            IEmailSender emailSender,
            UserManager<Person> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _subjectRepository = subjectRepository;
            _workingProgramRepository = workingProgramRepository;
            _commentRepository = commentRepository;
            _fileProvider = fileProvider;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<Result<Guid>> CreateAsync(WorkingProgramCreateModel model)
        {
            var createdBy = await _userManager.FindByEmailAsync(model.CreatedByEmail);
            var isSubjectExists = await _subjectRepository.ExistsAsync(s => s.Id == model.SubjectId);
            if (!isSubjectExists)
            {
                return Result.NotFound<Guid>(BlErrors.NotFound(model.SubjectId));
            }

            var isWpNameExists = await _workingProgramRepository.ExistsAsync(x => x.Name == model.Name);
            if (isWpNameExists)
            {
                return Result.ValidationError<Guid>(BlErrors.ExistingEntity);
            }

            var postFileResult = await _fileProvider.PostFileAsync(model.File);
            if (postFileResult.IsFailed)
            {
                return Result.Failure<Guid>(postFileResult.Errors.First());
            }

            var workingProgram = new WorkingProgram
            {
                Name = model.Name,
                FileName = postFileResult.Value.ToString(),
                SubjectId = model.SubjectId,
                CreatedById = createdBy.Id,
                IsAvailable = false,
            };

            return await _unitOfWork.NewTransaction(async () =>
            {
                var createdEntity = await _workingProgramRepository.AddAsync(workingProgram);

                return Result.Success(createdEntity.Id);
            });
        }

        public async Task<Result<WorkingProgramModel>> GetWorkingProgramFileAsync(Guid workingProgramId)
        {
            var workingProgram = await _workingProgramRepository.GetAsync(workingProgramId);
            if (workingProgram == null)
            {
                return Result.NotFound<WorkingProgramModel>(BlErrors.NotFound(workingProgramId));
            }

            var fileResult = await _fileProvider.GetFileAsync($"{workingProgram.FileName}.docx");
            if (fileResult.IsFailed)
            {
                return Result.Failure<WorkingProgramModel>(fileResult.Errors.First());
            }

            var file = new MemoryStream(fileResult.Value.Contents);
            var result = new WorkingProgramModel
            {
                FullFileName = $"{workingProgram.Name}.docx",
                File = file,
            };

            return Result.Success(result);
        }

        public async Task<Result<IEnumerable<WorkingProgramGetModel>>> GetAllAsync()
        {
            var workingPrograms = await _workingProgramRepository.GetAllAsync();
            var subjects = await _subjectRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<WorkingProgramGetModel>>(workingPrograms);
            foreach (var model in result)
            {
                var subjectId = workingPrograms.First(x => x.Id == model.Id).SubjectId;
                model.SubjectName = subjects.First(x => x.Id == subjectId).Name;
            }

            return Result.Success(result);
        }

        public async Task<Result<WorkingProgramDetailsModel>> GetByIdAsync(Guid id)
        {
            var workingProgram = await _workingProgramRepository.GetWithDetailsAsync(id,
                p => p.Comments,
                p => p.Comments,
                p => p.CreatedBy);
            if (workingProgram == null)
            {
                return Result.NotFound<WorkingProgramDetailsModel>(BlErrors.NotFound(id));
            }

            var subject = await _subjectRepository.GetWithDetailsAsync(workingProgram.SubjectId,
                s => s.EducationalProgram,
                s => s.EducationalProgram.EducationalProgramsType);
            var result = _mapper.Map<WorkingProgramDetailsModel>(workingProgram);
            result.Subject = _mapper.Map<IdNameModel<Guid>>(subject);
            result.EducationalProgram = _mapper.Map<IdNameModel<Guid>>(subject.EducationalProgram);
            result.CreatedByName = $"{workingProgram.CreatedBy.FirstName} {workingProgram.CreatedBy.LastName}";

            if (workingProgram.ApprovedById != null)
            {
                var approvedBy = _userManager.Users.First(x => x.Id == workingProgram.ApprovedById);
                result.ApprovedByName = $"{approvedBy.FirstName} {approvedBy.LastName}";
            }

            return Result.Success(result);
        }

        public async Task<Result> ApproveWorkingProgramAsync(Guid id, string approverEmail)
        {
            var workingProgram = await _workingProgramRepository.GetWithDetailsAsync(id);
            if (workingProgram == null)
            {
                return Result.NotFound(BlErrors.NotFound(id));
            }

            if (workingProgram.ApprovedById != null)
            {
                return Result.Success();
            }

            var approver = await _userManager.FindByEmailAsync(approverEmail);

            if (workingProgram.CreatedById == approver.Id)
            {
                return Result.ValidationError(BlErrors.CanNotApproveOwnWorkingProgram);
            }

            return await _unitOfWork.NewTransaction(() =>
            {
                workingProgram.IsAvailable = true;
                workingProgram.ApprovedById = approver.Id;

                return Result.SuccessTask();
            });
        }

        public async Task<Result<Guid>> CreateCommentAsync(CommentCreateModel model)
        {
            var person = await _userManager.FindByEmailAsync(model.CreatedByEmail);

            var entity = new Comment
            {
                Description = model.Description,
                CreatedById = person.Id,
                CreatedAt = DateTime.UtcNow,
                WorkingProgramId = model.WorkingProgramId,
            };

            return await _unitOfWork.NewTransaction(async () =>
            {
                var createdEntity = await _commentRepository.AddAsync(entity);

                return Result.Success(createdEntity.Id);
            });
        }

        public async Task<Result> DeleteByIdAsync(Guid id)
        {
            var isExisting = await _workingProgramRepository.ExistsAsync(x => x.Id == id);
            if (!isExisting)
            {
                return Result.NotFound(BlErrors.NotFound(id));
            }

            return await _unitOfWork.NewTransaction(async () =>
            {
                await _workingProgramRepository.DeleteAsync(id);
                return Result.Success();
            });
        }

        public async Task<Result> RejectAsync(RejectModel model, string email)
        {
            var workingProgram = await _workingProgramRepository.FindAsync(x => x.Id == model.WorkingProgramId);
            if (workingProgram == null)
            {
                return Result.NotFound(BlErrors.NotFound(model.WorkingProgramId));
            }

            var methodist = await _userManager.FindByEmailAsync(email);
            var author = await _userManager.FindByIdAsync(workingProgram.CreatedById.ToString());
            var rejectEmailModel = CreateRejectEmailModel(author.Email, workingProgram, model.Reason, $"{methodist.FirstName} {methodist.LastName}");

            return await _unitOfWork.NewTransaction(async () =>
            {
                await _workingProgramRepository.DeleteAsync(model.WorkingProgramId);
                await _emailSender.SendEmailAsync(rejectEmailModel);
                return Result.Success();
            });
        }

        private SendEmailModel CreateRejectEmailModel(string email, WorkingProgram model, string reason, string methodistFullName)
        {
            var result = new SendEmailModel
            {
                To = email,
                Subject = "Відхилення робочої програми",
                EmailBody = $"Вашу робочу програму({model.Name}) було відхилено методистом - {methodistFullName}, по причині:<br/>{reason}",
            };

            return result;
        }
    }
}