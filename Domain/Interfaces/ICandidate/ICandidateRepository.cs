using Domain.Models;

namespace Domain.Interfaces.ICandidateRepository
{
    public interface ICandidateRepository
    {
        // CREATE
        Task AddAsync(Candidate candidate);

        // READ
        
        Task<Candidate?> GetByIdAsync(Guid id);
        Task<Candidate?> GetWithSkillsAsync(Guid id);

        // DELETE Candidate
        Task RemoveAsync(Candidate candidate);

        // SEARCH (by name and/or skills)
        Task<IReadOnlyList<Candidate>> SearchAsync(
            string? name = null,
            IReadOnlyCollection<string>? skills = null);

        // ADD / REMOVE skills from candidate
        Task AddSkillsAsync(Guid candidateId, IReadOnlyCollection<string> skillNames);
        Task RemoveSkillAsync(Guid candidateId, Guid skillId);

        Task<bool> EmailExistsAsync(string email);
    }
}
