using AutoMapper;
using BLL.Contracts;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramResultController : ResultController
    {
        private readonly IProgramResultService _programResultService;

        public ProgramResultController(
            IMapper mapper,
            IProgramResultService programResultService) : base(mapper)
        {
            _programResultService = programResultService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _programResultService.GetAllAsync();

            return OperationResult<IEnumerable<ProgramResultGetModel>, IEnumerable<ProgramResultGetResponse>>(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProgramResultCreateRequest request)
        {
            var model = Mapper.Map<ProgramResultCreateModel>(request);
            var result = await _programResultService.CreateAsync(model);

            return OperationResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ProgramResultUpdateRequest request)
        {
            var model = Mapper.Map<ProgramResultUpdateModel>(request);
            model.Id = id;

            var result = await _programResultService.UpdateAsync(model);

            return OperationResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _programResultService.DeleteByIdAsync(id);

            return OperationResult(result);
        }
    }
}