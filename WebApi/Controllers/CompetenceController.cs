using AutoMapper;
using BLL.Contracts;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetenceController : ResultController
    {
        private readonly ICompetenceService _competenceService;

        public CompetenceController(
            IMapper mapper,
            ICompetenceService competenceService) : base(mapper)
        {
            _competenceService = competenceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _competenceService.GetAllAsync();

            return OperationResult<IEnumerable<CompetenceGetModel>, IEnumerable<CompetenceGetResponse>>(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CompetenceCreateRequest request)
        {
            var model = Mapper.Map<CompetenceCreateModel>(request);
            var result = await _competenceService.CreateAsync(model);

            return OperationResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CompetenceUpdateRequest request)
        {
            var model = Mapper.Map<CompetenceUpdateModel>(request);
            model.Id = id;

            var result = await _competenceService.UpdateAsync(model);

            return OperationResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _competenceService.DeleteByIdAsync(id);

            return OperationResult(result);
        }
    }
}