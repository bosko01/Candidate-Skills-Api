using CQRS.ICommand;
using Domain.Interfaces.ISkillRepository;
using Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Application.Skills.Commands.Remove;

public static class RemoveSkill
{
    public sealed record Command(Guid SkillId) : ICommand;

    public sealed class Handler(ISkillRepository repo, IUnitOfWork uow)
        : IRequestHandler<Command>
    {
        public async Task Handle(Command c, CancellationToken ct)
        {
            var skill = await repo.GetByIdAsync(c.SkillId)
                         ?? throw new KeyNotFoundException("Skill not found.");

            await repo.RemoveAsync(skill);
            await uow.SaveChangesAsync();
        }
    }
}
