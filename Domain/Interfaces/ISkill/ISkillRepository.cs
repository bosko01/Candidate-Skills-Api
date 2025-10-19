using Domain.Models;

namespace Domain.Interfaces.ISkillRepository
{
    public interface ISkillRepository
    {
        // CREATE
        Task AddAsync(Skill skill);

        // READ
        Task<Skill?> GetByIdAsync(Guid id);
        Task<Skill?> FindByNameAsync(string name);
        Task<IReadOnlyList<Skill>> GetAllAsync();

        // UPSERT 
        Task<Skill> GetOrCreateAsync(string name);

        // DELETE 
        Task RemoveAsync(Skill skill);
    }
}
