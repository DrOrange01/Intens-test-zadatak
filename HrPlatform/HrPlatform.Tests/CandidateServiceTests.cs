using HrPlatform.Application.DTOs;
using HrPlatform.Application.DTOs.Candidates;
using HrPlatform.Domain.Entities;
using HrPlatform.Infrastructure.Data;
using HrPlatform.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrPlatform.Tests
{
    public class CandidateServiceTests
    {
        private AppDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddCandidate()
        {
            var context = CreateInMemoryContext();
            var service = new CandidateService(context);

            var dto = new CreateCandidateDto
            {
                Name = "Test User",
                DateOfBirth = new DateTime(1995, 1, 1),
                ContactNumber = "0601234567",
                Email = "test@email.com"
            };

            var result = await service.CreateAsync(dto);

            Assert.NotNull(result);
            Assert.Equal("Test User", result.Name);
            Assert.Equal("test@email.com", result.Email);
        }

        [Fact]
        public async Task CreateAsync_DuplicateEmail_ShouldThrow()
        {
            var context = CreateInMemoryContext();
            var service = new CandidateService(context);

            var dto = new CreateCandidateDto
            {
                Name = "Test User",
                DateOfBirth = new DateTime(1995, 1, 1),
                ContactNumber = "0601234567",
                Email = "test@email.com"
            };

            await service.CreateAsync(dto);

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.CreateAsync(dto));
        }

        [Fact]
        public async Task DeleteAsync_ExistingCandidate_ShouldReturnTrue()
        {
            var context = CreateInMemoryContext();
            var service = new CandidateService(context);

            var candidate = new Candidate
            {
                Name = "Test User",
                DateOfBirth = new DateTime(1995, 1, 1),
                ContactNumber = "0601234567",
                Email = "test@email.com"
            };
            context.Candidates.Add(candidate);
            await context.SaveChangesAsync();

            var result = await service.DeleteAsync(candidate.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_NonExistingCandidate_ShouldReturnFalse()
        {
            var context = CreateInMemoryContext();
            var service = new CandidateService(context);

            var result = await service.DeleteAsync(999);

            Assert.False(result);
        }

        [Fact]
        public async Task SearchAsync_ByName_ShouldReturnMatchingCandidates()
        {
            var context = CreateInMemoryContext();
            var service = new CandidateService(context);

            context.Candidates.AddRange(
                new Candidate { Name = "Ana Petrović", DateOfBirth = new DateTime(1995, 1, 1), ContactNumber = "060111", Email = "ana@email.com" },
                new Candidate { Name = "Marko Nikolić", DateOfBirth = new DateTime(1992, 1, 1), ContactNumber = "060222", Email = "marko@email.com" }
            );
            await context.SaveChangesAsync();

            var results = await service.SearchAsync("Ana", null);

            Assert.Single(results);
            Assert.Equal("Ana Petrović", results.First().Name);
        }

        [Fact]
        public async Task AddSkillToCandidate_ShouldReturnTrue()
        {
            var context = CreateInMemoryContext();
            var service = new CandidateService(context);

            var candidate = new Candidate { Name = "Test", DateOfBirth = new DateTime(1995, 1, 1), ContactNumber = "060111", Email = "test@email.com" };
            var skill = new Skill { Name = "C# Programming" };
            context.Candidates.Add(candidate);
            context.Skills.Add(skill);
            await context.SaveChangesAsync();

            var result = await service.AddSkillToCandidateAsync(candidate.Id, skill.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task AddSkillToCandidate_Duplicate_ShouldReturnFalse()
        {
            var context = CreateInMemoryContext();
            var service = new CandidateService(context);

            var candidate = new Candidate { Name = "Test", DateOfBirth = new DateTime(1995, 1, 1), ContactNumber = "060111", Email = "test@email.com" };
            var skill = new Skill { Name = "C# Programming" };
            context.Candidates.Add(candidate);
            context.Skills.Add(skill);
            await context.SaveChangesAsync();

            await service.AddSkillToCandidateAsync(candidate.Id, skill.Id);
            var result = await service.AddSkillToCandidateAsync(candidate.Id, skill.Id);

            Assert.False(result);
        }
    }
}
