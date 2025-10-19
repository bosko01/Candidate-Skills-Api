using Application.DTOs;
using Application.DTOs.Mappers;
using CQRS.IQuery;
using Domain.Interfaces.ICandidateRepository;
using MediatR;

namespace Application.Candidates.Queries.GetAll;

public static class GetAllCandidates
{
    // Record koji predstavlja Query
    public sealed record Query() : IQuery<IReadOnlyList<CandidateListItemDto>>;

    // Handler koji obrađuje Query
    public sealed class Handler(ICandidateRepository repo)
        : IRequestHandler<Query, IReadOnlyList<CandidateListItemDto>>
    {
        public async Task<IReadOnlyList<CandidateListItemDto>> Handle(Query request, CancellationToken ct)
        {
            var candidates = await repo.SearchAsync(null, null);
            return candidates
                .Select(c => c.ToListItemDto())
                .OrderBy(c => c.FullName)
                .ToList();
        }
    }
}
