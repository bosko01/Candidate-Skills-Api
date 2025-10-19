using CQRS.ICommand;
using Domain.Interfaces.ICandidateRepository;
using Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Application.Candidates.Commands.RemoveSkill;

public static class RemoveSkillFromCandidate
{
    public sealed record Command(Guid CandidateId, Guid SkillId) : ICommand;

    public sealed class Handler(ICandidateRepository repo, IUnitOfWork uow)
        : IRequestHandler<Command>
    {
        public async Task Handle(Command c, CancellationToken ct)
        {
            await repo.RemoveSkillAsync(c.CandidateId, c.SkillId);
            await uow.SaveChangesAsync();
        }
    }
}
