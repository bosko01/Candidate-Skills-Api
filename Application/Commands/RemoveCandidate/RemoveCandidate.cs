using CQRS.ICommand;
using Domain.Interfaces.ICandidateRepository;
using Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Application.Candidates.Commands.Remove;

public static class RemoveCandidate
{
    public sealed record Command(Guid CandidateId) : ICommand;

    public sealed class Handler(ICandidateRepository repo, IUnitOfWork uow)
        : IRequestHandler<Command>
    {
        public async Task Handle(Command c, CancellationToken ct)
        {
            var entity = await repo.GetByIdAsync(c.CandidateId)
                         ?? throw new KeyNotFoundException("Candidate not found.");
            await repo.RemoveAsync(entity);
            await uow.SaveChangesAsync();
        }
    }
}
