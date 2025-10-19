using Application.DTOs;
using Application.DTOs.Mappers;
using CQRS.IQuery;
using Domain.Interfaces.ICandidateRepository;
using MediatR;

namespace Application.Candidates.Queries.GetById;

public static class GetCandidateById
{
    public sealed record Query(Guid Id) : IQuery<CandidateDetailsDto?>;

    public sealed class Handler(ICandidateRepository repo)
        : IRequestHandler<Query, CandidateDetailsDto?>
    {
        public async Task<CandidateDetailsDto?> Handle(Query q, CancellationToken ct)
            => (await repo.GetWithSkillsAsync(q.Id))?.ToDetailsDto();
    }
}
