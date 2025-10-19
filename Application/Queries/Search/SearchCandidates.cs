using Application.DTOs;
using Application.DTOs.Mappers;
using CQRS.IQuery;
using Domain.Interfaces.ICandidateRepository;
using MediatR;

namespace Application.Candidates.Queries.Search;

public static class SearchCandidates
{
    public sealed record Query(string? Name, string? SkillsCsv)
        : IQuery<IReadOnlyList<CandidateListItemDto>>;

    public sealed class Handler(ICandidateRepository repo)
        : IRequestHandler<Query, IReadOnlyList<CandidateListItemDto>>
    {
        public async Task<IReadOnlyList<CandidateListItemDto>> Handle(Query q, CancellationToken ct)
        {
            var skills = string.IsNullOrWhiteSpace(q.SkillsCsv)
                ? null
                : q.SkillsCsv.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();

            var list = await repo.SearchAsync(q.Name, skills);
            return list.Select(c => c.ToListItemDto()).ToList();
        }
    }
}
