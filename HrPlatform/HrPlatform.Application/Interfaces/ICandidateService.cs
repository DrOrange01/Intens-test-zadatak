using HrPlatform.Application.DTOs.Candidates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrPlatform.Application.Interfaces
{
    public interface ICandidateService
    {
        Task<IEnumerable<CandidateDto>> GetAllAsync();
        Task<CandidateDto?> GetByIdAsync(int id);
        Task<CandidateDto> CreateAsync(CreateCandidateDto dto);
        Task<CandidateDto?> UpdateAsync(int id, UpdateCandidateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> AddSkillToCandidateAsync(int candidateId, int skillId);
        Task<bool> RemoveSkillFromCandidateAsync(int candidateId, int skillId);
        Task<IEnumerable<CandidateDto>> SearchAsync(string? name, List<int>? skillIds);
    }
}
