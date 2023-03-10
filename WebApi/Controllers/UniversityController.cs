using AutoMapper;
using BLL.Contracts;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.RequestResponse;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ResultController
    {
        private readonly IUniversityService _universityService;

        public UniversityController(IMapper mapper, IUniversityService universityService) : base(mapper)
        {
            _universityService = universityService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _universityService.GetAllAsync();

            return OperationResult<IEnumerable<UniversityModel>, IEnumerable<UniversityResponse>>(result);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SuccessResponse<UniversityResponse>), 200)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _universityService.GetByIdAsync(id);

            return OperationResult<UniversityModel, UniversityResponse>(result);
        }
        
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> CreateAsync(UniversityRequest request)
        {
            var model = Mapper.Map<UniversityModel>(request);
            var result = await _universityService.CreateAsync(model);

            return OperationResult(result);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UniversityRequest request)
        {
            var model = Mapper.Map<UniversityModel>(request);
            model.Id = id;

            var result = await _universityService.UpdateAsync(model);

            return OperationResult(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _universityService.DeleteByIdAsync(id);

            return OperationResult(result);
        }
    }
}