using HrPlatform.Application.DTOs.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrPlatform.Application.Interfaces
{
    public interface ISkillService
    {
        Task<IEnumerable<SkillDto>> GetAllAsync();
        Task<SkillDto?> GetByIdAsync(int id);
        Task<SkillDto> CreateAsync(CreateSkillDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
