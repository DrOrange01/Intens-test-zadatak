using HrPlatform.Application.DTOs.Skills;
using HrPlatform.Application.Interfaces;
using HrPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrPlatform.Infrastructure.Services
{
    public class SkillService : ISkillService
    {
        private readonly AppDbContext _context;

        public SkillService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SkillDto>> GetAllAsync()
        {
            return await _context.Skills
                .Select(s => new SkillDto { Id = s.Id, Name = s.Name })
                .ToListAsync();
        }

        public async Task<SkillDto?> GetByIdAsync(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            return skill == null ? null : new SkillDto { Id = skill.Id, Name = skill.Name };
        }

        public async Task<SkillDto> CreateAsync(CreateSkillDto dto)
        {
            var existing = await _context.Skills
                .AnyAsync(s => s.Name.ToLower() == dto.Name.ToLower());

            if (existing)
                throw new InvalidOperationException($"Skill '{dto.Name}' already exists.");

            var skill = new Domain.Entities.Skill { Name = dto.Name };
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return new SkillDto { Id = skill.Id, Name = skill.Name };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null) return false;

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
