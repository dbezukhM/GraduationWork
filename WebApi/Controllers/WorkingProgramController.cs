using AutoMapper;
using BLL.Contracts;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingProgramController : ResultController
    {
        private readonly IWorkingProgramService _workingProgramService;
        private readonly IFileGenerator _fileGenerator;

        public WorkingProgramController(
            IMapper mapper,
            IWorkingProgramService workingProgramService,
            IFileGenerator fileGenerator) : base(mapper)
        {
            _workingProgramService = workingProgramService;
            _fileGenerator = fileGenerator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _workingProgramService.GetAllAsync();

            return OperationResult<IEnumerable<WorkingProgramGetModel>, IEnumerable<WorkingProgramGetResponse>>(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _workingProgramService.GetByIdAsync(id);

            return OperationResult<WorkingProgramDetailsModel, WorkingProgramDetailsResponse>(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Lecturer")]
        public async Task<IActionResult> CreateAsync([FromForm] WorkingProgramCreateRequest model)
        {
            var workingProgramModel = Mapper.Map<WorkingProgramCreateModel>(model);
            workingProgramModel.CreatedByEmail = User.FindFirst(ClaimTypes.Name)?.Value;
            var result = await _workingProgramService.CreateAsync(workingProgramModel);

            return OperationResult(result);
        }

        [HttpGet("generateTemplate/{subjectId}")]
        [Authorize(Roles = "Admin, Lecturer")]
        public async Task<IActionResult> GenerateTemplateAsync(Guid subjectId)
        {
            var result = await _fileGenerator.GenerateFile(subjectId);

            if (result.IsFailed)
            {
                return ErrorResult(result);
            }

            return File(result.Value.File.ToArray(), "application/vnd.ms-word", result.Value.FullFileName);
        }

        [HttpGet("getWorkingProgramFile/{workingProgramId}")]
        public async Task<IActionResult> GetWorkingProgramFileAsync(Guid workingProgramId)
        {
            var result = await _workingProgramService.GetWorkingProgramFileAsync(workingProgramId);

            if (result.IsFailed)
            {
                return ErrorResult(result);
            }
            
            return File(result.Value.File.ToArray(), "application/vnd.ms-word", result.Value.FullFileName);
        }

        [HttpPut("approve/{id}")]
        [Authorize(Roles = "Admin, Lecturer")]
        public async Task<IActionResult> ApproveWorkingProgramAsync([FromRoute] Guid id)
        {
            var result = await _workingProgramService.ApproveWorkingProgramAsync(id, User.FindFirst(ClaimTypes.Name)?.Value);

            return OperationResult(result);
        }

        [HttpPost("createComment")]
        [Authorize(Roles = "Admin, Lecturer")]
        public async Task<IActionResult> CreateCommentAsync(CommentCreateRequest request)
        {
            var commentCreateModel = Mapper.Map<CommentCreateModel>(request);
            commentCreateModel.CreatedByEmail = User.FindFirst(ClaimTypes.Name)?.Value;
            var result = await _workingProgramService.CreateCommentAsync(commentCreateModel);

            return OperationResult(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Lecturer")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _workingProgramService.DeleteByIdAsync(id);

            return OperationResult(result);
        }
    }
}