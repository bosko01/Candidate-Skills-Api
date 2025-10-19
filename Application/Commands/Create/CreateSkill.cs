using CQRS.ICommand;
using CQRS.ICommand;
using Domain.Interfaces.ISkillRepository;
using Domain.Interfaces.UnitOfWork;
using Domain.Models;
using MediatR;

namespace Application.Skills.Commands.Create;

public static class CreateSkill
{
    public sealed record Command(string Name) : ICommand<Guid>;

    public sealed class Handler(ISkillRepository repo, IUnitOfWork uow)
        : IRequestHandler<Command, Guid>
    {
        public async Task<Guid> Handle(Command c, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(c.Name))
                throw new ArgumentException("Skill name is required.", nameof(c.Name));

            var skill = await repo.GetOrCreateAsync(c.Name);
            await uow.SaveChangesAsync();

            return skill.Id;
        }
    }
}
