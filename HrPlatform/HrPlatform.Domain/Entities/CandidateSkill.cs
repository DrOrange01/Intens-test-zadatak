using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrPlatform.Domain.Entities
{
    public class CandidateSkill
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int SkillId { get; set; }
        public Candidate Candidate { get; set; } = null!;
        public Skill Skill { get; set; } = null!;
    }
}
