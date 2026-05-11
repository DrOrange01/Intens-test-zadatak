using HrPlatform.Application.DTOs.Skills;
using HrPlatform.Application.DTOs.Candidates;
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
    public class CandidateService : ICandidateService
    {
        private readonly AppDbContext _context;

        public CandidateService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CandidateDto>> GetAllAsync()
        {
            return await _context.Candidates
                .Include(c => c.CandidateSkills)
                    .ThenInclude(cs => cs.Skill)
                .Select(c => ToDto(c))
                .ToListAsync();
        }

        public async Task<CandidateDto?> GetByIdAsync(int id)
        {
            var candidate = await _context.Candidates
                .Include(c => c.CandidateSkills)
                    .ThenInclude(cs => cs.Skill)
                .FirstOrDefaultAsync(c => c.Id == id);

            return candidate == null ? null : ToDto(candidate);
        }

        public async Task<CandidateDto> CreateAsync(CreateCandidateDto dto)
        {
            var candidate = new Domain.Entities.Candidate
            {
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth,
                ContactNumber = dto.ContactNumber,
                Email = dto.Email
            };

            var emailExists = await _context.Candidates.AnyAsync(c => c.Email.ToLower() == dto.Email.ToLower());
            if (emailExists)
                throw new InvalidOperationException($"Candidate with email '{dto.Email}' already exists.");

            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();

            return ToDto(candidate);
        }

        public async Task<CandidateDto?> UpdateAsync(int id, UpdateCandidateDto dto)
        {
            var candidate = await _context.Candidates
                .Include(c => c.CandidateSkills)
                    .ThenInclude(cs => cs.Skill)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (candidate == null) return null;

            candidate.Name = dto.Name;
            candidate.DateOfBirth = dto.DateOfBirth;
            candidate.ContactNumber = dto.ContactNumber;
            candidate.Email = dto.Email;

            await _context.SaveChangesAsync();

            return ToDto(candidate);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null) return false;

            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddSkillToCandidateAsync(int candidateId, int skillId)
        {
            var candidate = await _context.Candidates.FindAsync(candidateId);
            var skill = await _context.Skills.FindAsync(skillId);

            if (candidate == null || skill == null) return false;

            var exists = await _context.CandidateSkills
                .AnyAsync(cs => cs.CandidateId == candidateId && cs.SkillId == skillId);

            if (exists) return false;

            _context.CandidateSkills.Add(new Domain.Entities.CandidateSkill
            {
                CandidateId = candidateId,
                SkillId = skillId
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveSkillFromCandidateAsync(int candidateId, int skillId)
        {
            var candidateSkill = await _context.CandidateSkills
                .FirstOrDefaultAsync(cs => cs.CandidateId == candidateId && cs.SkillId == skillId);

            if (candidateSkill == null) return false;

            _context.CandidateSkills.Remove(candidateSkill);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CandidateDto>> SearchAsync(string? name, List<int>? skillIds)
        {
            var query = _context.Candidates
                .Include(c => c.CandidateSkills)
                    .ThenInclude(cs => cs.Skill)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(c => c.Name.ToLower().Contains(name.ToLower()));

            if (skillIds != null && skillIds.Any())
                query = query.Where(c => skillIds.All(sid =>
                    c.CandidateSkills.Any(cs => cs.SkillId == sid)));

            return await query.Select(c => ToDto(c)).ToListAsync();
        }

        private static CandidateDto ToDto(Domain.Entities.Candidate c) => new()
        {
            Id = c.Id,
            Name = c.Name,
            DateOfBirth = c.DateOfBirth,
            ContactNumber = c.ContactNumber,
            Email = c.Email,
            Skills = c.CandidateSkills.Select(cs => new SkillDto
            {
                Id = cs.Skill.Id,
                Name = cs.Skill.Name
            }).ToList()
        };
    }
}
