using Domain.Interfaces.ISkillRepository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.SkillRepository
{
    public sealed class SkillRepository : ISkillRepository
    {
        private readonly AppDbContext _db;
        public SkillRepository(AppDbContext db) => _db = db;

        public Task AddAsync(Skill skill)
            => _db.Skills.AddAsync(skill).AsTask();

        public Task<Skill?> GetByIdAsync(Guid id)
            => _db.Skills.FirstOrDefaultAsync(s => s.Id == id);

        public Task<Skill?> FindByNameAsync(string name)
        {
            var n = name.Trim().ToLower();
            return _db.Skills.FirstOrDefaultAsync(s => s.Name.ToLower() == n);
        }

        public async Task<IReadOnlyList<Skill>> GetAllAsync()
            => await _db.Skills.AsNoTracking().OrderBy(s => s.Name).ToListAsync();

        public async Task<Skill> GetOrCreateAsync(string name)
        {
            var ex = await FindByNameAsync(name);
            if (ex is not null) return ex;
            var entity = Skill.Create(name);
            await _db.Skills.AddAsync(entity);
            return entity;
        }

        public Task RemoveAsync(Skill skill)
        {
            _db.Skills.Remove(skill);
            return Task.CompletedTask;
        }
    }
}
