using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrPlatform.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Candidate> Candidates => Set<Candidate>();
        public DbSet<Skill> Skills => Set<Skill>();
        public DbSet<CandidateSkill> CandidateSkills => Set<CandidateSkill>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandidateSkill>()
                .HasKey(cs => new { cs.CandidateId, cs.SkillId });

            modelBuilder.Entity<Candidate>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Skill>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "C# Programming" },
                new Skill { Id = 2, Name = "Java Programming" },
                new Skill { Id = 3, Name = "Database Design" },
                new Skill { Id = 4, Name = "React" },
                new Skill { Id = 5, Name = "English" },
                new Skill { Id = 6, Name = "German" },
                new Skill { Id = 7, Name = "Russian" },
                new Skill { Id = 8, Name = "Project Management" }
            );

            modelBuilder.Entity<Candidate>().HasData(
            new Candidate { Id = 1, Name = "Ana Petrović", DateOfBirth = new DateTime(1995, 3, 15), ContactNumber = "0601234567", Email = "ana.petrovic@email.com" },
            new Candidate { Id = 2, Name = "Marko Nikolić", DateOfBirth = new DateTime(1992, 7, 22), ContactNumber = "0637654321", Email = "marko.nikolic@email.com" },
            new Candidate { Id = 3, Name = "Jelena Jović", DateOfBirth = new DateTime(1998, 11, 5), ContactNumber = "0651112233", Email = "jelena.jovic@email.com" },
            new Candidate { Id = 4, Name = "Stefan Đorđević", DateOfBirth = new DateTime(1990, 1, 30), ContactNumber = "0623334455", Email = "stefan.djordjevic@email.com" },
            new Candidate { Id = 5, Name = "Milica Stojanović", DateOfBirth = new DateTime(1997, 6, 18), ContactNumber = "0615556677", Email = "milica.stojanovic@email.com" }
            );

            modelBuilder.Entity<CandidateSkill>().HasData(
            new CandidateSkill { CandidateId = 1, SkillId = 1 },
            new CandidateSkill { CandidateId = 1, SkillId = 3 },
            new CandidateSkill { CandidateId = 1, SkillId = 5 },
            new CandidateSkill { CandidateId = 2, SkillId = 2 },
            new CandidateSkill { CandidateId = 2, SkillId = 3 },
            new CandidateSkill { CandidateId = 3, SkillId = 1 },
            new CandidateSkill { CandidateId = 3, SkillId = 4 },
            new CandidateSkill { CandidateId = 3, SkillId = 5 },
            new CandidateSkill { CandidateId = 4, SkillId = 8 },
            new CandidateSkill { CandidateId = 4, SkillId = 5 },
            new CandidateSkill { CandidateId = 4, SkillId = 6 },
            new CandidateSkill { CandidateId = 5, SkillId = 1 },
            new CandidateSkill { CandidateId = 5, SkillId = 4 },
            new CandidateSkill { CandidateId = 5, SkillId = 7 }
            );
        }
    }
}
