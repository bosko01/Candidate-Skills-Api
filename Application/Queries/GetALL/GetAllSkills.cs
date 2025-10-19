using CQRS.ICommand;
using Application.DTOs;
using CQRS.IQuery;
using Domain.Interfaces.ISkillRepository;
using MediatR;

namespace Application.Skills.Queries.GetAll;

public static class GetAllSkills
{
    public sealed record Query() : IQuery<IReadOnlyList<SkillDto>>;

    public sealed class Handler(ISkillRepository repo)
        : IRequestHandler<Query, IReadOnlyList<SkillDto>>
    {
        public async Task<IReadOnlyList<SkillDto>> Handle(Query q, CancellationToken ct)
        {
            var skills = await repo.GetAllAsync();
            return skills
                .OrderBy(s => s.Name)
                .Select(s => new SkillDto(s.Id, s.Name))
                .ToList();
        }
    }
}
