using Domain.Interfaces.ICandidateRepository;
using Domain.Interfaces.ISkillRepository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.CandidateRepository
{
    public sealed class CandidateRepository : ICandidateRepository
    {
        private readonly AppDbContext _db;
        private readonly ISkillRepository _skills;

        public CandidateRepository(AppDbContext db, ISkillRepository skills)
        {
            _db = db;
            _skills = skills;
        }

        public Task AddAsync(Candidate candidate)
            => _db.Candidates.AddAsync(candidate).AsTask();

        public Task<Candidate?> GetByIdAsync(Guid id)
            => _db.Candidates.FirstOrDefaultAsync(c => c.Id == id);

        public Task<Candidate?> GetWithSkillsAsync(Guid id)
            => _db.Candidates
                  .Include(c => c.CandidateSkills)
                  .ThenInclude(cs => cs.Skill)
                  .FirstOrDefaultAsync(c => c.Id == id);

        public Task RemoveAsync(Candidate candidate)
        {
            _db.Candidates.Remove(candidate);
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<Candidate>> SearchAsync(string? name = null, IReadOnlyCollection<string>? skills = null)
        {
            IQueryable<Candidate> q = _db.Candidates
                .Include(c => c.CandidateSkills).ThenInclude(cs => cs.Skill)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(name))
            {
                var n = name.Trim().ToLower();
                q = q.Where(c => c.FirstName.ToLower().Contains(n) || c.LastName.ToLower().Contains(n));
            }

            if (skills is { Count: > 0 })
            {
                var need = skills.Where(s => !string.IsNullOrWhiteSpace(s))
                                 .Select(s => s.Trim().ToLower())
                                 .Distinct()
                                 .ToList();

                foreach (var s in need)
                    q = q.Where(c => c.CandidateSkills.Any(cs => cs.Skill.Name.ToLower() == s));
            }

            return await q.ToListAsync();
        }

        public async Task AddSkillsAsync(Guid candidateId, IReadOnlyCollection<string> skillNames)
        {
            var candidate = await _db.Candidates
                .Include(c => c.CandidateSkills).ThenInclude(cs => cs.Skill)
                .FirstOrDefaultAsync(c => c.Id == candidateId)
                ?? throw new KeyNotFoundException("Candidate not found.");

            var distinct = skillNames
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase);

            foreach (var name in distinct)
            {
                var skill = await _skills.GetOrCreateAsync(name);
                if (!candidate.CandidateSkills.Any(cs => cs.Skill.Id == skill.Id))
                    candidate.AddSkill(skill);
            }
        }

        public async Task RemoveSkillAsync(Guid candidateId, Guid skillId)
        {
            var candidate = await _db.Candidates
                .Include(c => c.CandidateSkills).ThenInclude(cs => cs.Skill)
                .FirstOrDefaultAsync(c => c.Id == candidateId)
                ?? throw new KeyNotFoundException("Candidate not found.");

            var link = candidate.CandidateSkills.FirstOrDefault(cs => cs.Skill.Id == skillId)
                       ?? throw new KeyNotFoundException("Skill not linked to candidate.");

            candidate.RemoveSkill(link.Skill);
        }

        public Task<bool> EmailExistsAsync(string email)
        {
            var normalized = email.Trim().ToLower();
            return _db.Candidates.AnyAsync(c => c.Email.mailAddress == normalized);
        }
    }
}
