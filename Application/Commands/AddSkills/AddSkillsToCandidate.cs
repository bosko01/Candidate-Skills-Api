using CQRS;
using Application.DTOs;
using Domain.Interfaces.ICandidateRepository;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using CQRS.ICommand;

namespace Application.Candidates.Commands.AddSkills;

public static class AddSkillsToCandidate
{
    public sealed record Command(Guid CandidateId, AddSkillsDto Dto) : ICommand;

    public sealed class Handler(ICandidateRepository repo, IUnitOfWork uow)
        : IRequestHandler<Command>
    {
        public async Task Handle(Command c, CancellationToken ct)
        {
            await repo.AddSkillsAsync(c.CandidateId, c.Dto.Skills);
            await uow.SaveChangesAsync();
        }
    }
}
