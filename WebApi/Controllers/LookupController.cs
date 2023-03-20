using AutoMapper;
using BLL.Contracts;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ResultController
    {
        private readonly ILookupService _lookupService;

        public LookupController(
            IMapper mapper,
            ILookupService lookupService) : base(mapper)
        {
            _lookupService = lookupService;
        }

        [HttpGet("areaOfExpertise")]
        public async Task<IActionResult> GetAreaOfExpertiseAsync()
        {
            var result = await _lookupService.GetAreaOfExpertiseAsync();

            return OperationResult(result);
        }

        [HttpGet("specializations/{id}")]
        public async Task<IActionResult> GetSpecializationsAsync(Guid id)
        {
            var result = await _lookupService.GetSpecializationsAsync(id);

            return OperationResult(result);
        }

        [HttpGet("universities")]
        public async Task<IActionResult> GetUniversitiesAsync()
        {
            var result = await _lookupService.GetUniversitiesAsync();

            return OperationResult(result);
        }

        [HttpGet("faculties/{id}")]
        public async Task<IActionResult> GetFacultiesAsync(Guid id)
        {
            var result = await _lookupService.GetFacultiesAsync(id);

            return OperationResult(result);
        }

        [HttpGet("educationalProgramsTypes")]
        public async Task<IActionResult> GetEducationalProgramsTypesAsync()
        {
            var result = await _lookupService.GetEducationalProgramsTypesAsync();

            return OperationResult(result);
        }

        [HttpGet("educationalPrograms")]
        public async Task<IActionResult> GetEducationalProgramsAsync()
        {
            var result = await _lookupService.GetEducationalProgramsAsync();

            return OperationResult(result);
        }

        [HttpGet("competences/{id}")]
        public async Task<IActionResult> GetCompetencesAsync(Guid id)
        {
            var result = await _lookupService.GetCompetencesAsync(id);

            return OperationResult<IEnumerable<CompetenceModel>, IEnumerable<CompetenceResponse>>(result);
        }

        [HttpGet("programResults/{id}")]
        public async Task<IActionResult> GetProgramResultsAsync(Guid id)
        {
            var result = await _lookupService.GetProgramResultsAsync(id);

            return OperationResult<IEnumerable<ProgramResultModel>, IEnumerable<ProgramResultResponse>>(result);
        }

        [HttpGet("finalControlTypes")]
        public async Task<IActionResult> GetFinalControlTypesAsync()
        {
            var result = await _lookupService.GetFinalControlTypesAsync();

            return OperationResult(result);
        }

        [HttpGet("selectiveBlocks")]
        public async Task<IActionResult> GetSelectiveBlocksAsync()
        {
            var result = await _lookupService.GetSelectiveBlocksAsync();

            return OperationResult(result);
        }
    }
}