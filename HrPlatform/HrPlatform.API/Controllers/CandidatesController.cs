using HrPlatform.Application.DTOs.Candidates;
using HrPlatform.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HrPlatform.Application.DTOs.Skills;

namespace HrPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var candidates = await _candidateService.GetAllAsync();
            return Ok(candidates);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var candidate = await _candidateService.GetByIdAsync(id);
            if (candidate == null) return NotFound();
            return Ok(candidate);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCandidateDto dto)
        {
            try
            {
                var candidate = await _candidateService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = candidate.Id }, candidate);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCandidateDto dto)
        {
            var candidate = await _candidateService.UpdateAsync(id, dto);
            return candidate == null ? NotFound() : Ok(candidate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _candidateService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpPost("{candidateId}/skills/{skillId}")]
        public async Task<IActionResult> AddSkill(int candidateId, int skillId)
        {
            try
            {
                var candidate = await _candidateService.AddSkillToCandidateAsync(candidateId, skillId);
                if (!candidate) return BadRequest("Skill already assigned or candidate/skill not found.");
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{candidateId}/skills/{skillId}")]
        public async Task<IActionResult> RemoveSkill(int candidateId, int skillId)
        {
            try
            {
                var candidate = await _candidateService.RemoveSkillFromCandidateAsync(candidateId, skillId);
                if (!candidate) return NotFound();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/search")]
        public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] List<int>? skillIds)
        {
            var candidates = await _candidateService.SearchAsync(name, skillIds);
            return Ok(candidates);
        }
    }
}
