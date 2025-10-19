using Application.DTOs;
using Domain.Models;

namespace Application.DTOs.Mappers;

public static class DtoMappers
{
    public static CandidateDetailsDto ToDetailsDto(this Candidate c) =>
        new(
            c.Id,
            c.FirstName,
            c.LastName,
            c.Email.mailAddress,
            c.PhoneNumber,
            c.DateOfBirth,
            c.CandidateSkills
             .Select(cs => new SkillDto(cs.Skill.Id, cs.Skill.Name))
             .OrderBy(s => s.Name)
             .ToList()
        );

    public static CandidateListItemDto ToListItemDto(this Candidate c) =>
        new(
            c.Id,
            $"{c.FirstName} {c.LastName}",
            c.Email.mailAddress,
            c.CandidateSkills
             .Select(cs => cs.Skill.Name)
             .OrderBy(n => n)
             .ToList()
        );
}
