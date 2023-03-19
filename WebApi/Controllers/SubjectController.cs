using AutoMapper;
using BLL.Contracts;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ResultController
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(
            IMapper mapper,
            ISubjectService subjectService) : base(mapper)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _subjectService.GetAllAsync();

            return OperationResult<IEnumerable<SubjectModel>, IEnumerable<SubjectResponse>>(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _subjectService.GetByIdAsync(id);

            return OperationResult<SubjectGetModel, SubjectGetResponse>(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(SubjectCreateRequest request)
        {
            var model = Mapper.Map<SubjectCreateModel>(request);
            var result = await _subjectService.CreateAsync(model);

            return OperationResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] SubjectUpdateRequest request)
        {
            var model = Mapper.Map<SubjectUpdateModel>(request);
            model.Id = id;

            var result = await _subjectService.UpdateAsync(model);

            return OperationResult(result);
        }
    }
}