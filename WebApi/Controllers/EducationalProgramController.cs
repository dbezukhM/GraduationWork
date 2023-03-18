using AutoMapper;
using BLL.Contracts;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationalProgramController : ResultController
    {
        private readonly IEducationalProgramService _educationalProgramService;

        public EducationalProgramController(
            IMapper mapper,
            IEducationalProgramService educationalProgramService) : base(mapper)
        {
            _educationalProgramService = educationalProgramService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _educationalProgramService.GetAllAsync();

            return OperationResult<IEnumerable<EducationalProgramModel>, IEnumerable<EducationalProgramResponse>>(
                result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _educationalProgramService.GetByIdAsync(id);

            return OperationResult<EducationalProgramGetModel, EducationalProgramGetResponse>(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(EducationalProgramCreateRequest request)
        {
            var model = Mapper.Map<EducationalProgramCreateModel>(request);
            var result = await _educationalProgramService.CreateAsync(model);

            return OperationResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] EducationalProgramUpdateRequest request)
        {
            var model = Mapper.Map<EducationalProgramUpdateModel>(request);
            model.Id = id;

            var result = await _educationalProgramService.UpdateAsync(model);

            return OperationResult(result);
        }
    }
}