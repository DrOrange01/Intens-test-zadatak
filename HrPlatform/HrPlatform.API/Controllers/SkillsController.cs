using Microsoft.AspNetCore.Mvc;
using HrPlatform.Application.Interfaces;
using HrPlatform.Application.DTOs.Skills;


namespace HrPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var skills = await _skillService.GetAllAsync();
            return Ok(skills);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var skill = await _skillService.GetByIdAsync(id);
            if (skill == null) return NotFound();
            return Ok(skill);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSkillDto dto)
        {
            try
            {
                var skill = await _skillService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = skill.Id }, skill);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _skillService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
