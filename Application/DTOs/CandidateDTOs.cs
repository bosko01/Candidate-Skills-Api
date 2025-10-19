using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public sealed record CreateCandidateDto(
        string FirstName,
        string LastName,
        string Email,
        string? PhoneNumber,
        DateOnly? DateOfBirth
    );

    public sealed record UpdateCandidateDto(
        string FirstName,
        string LastName,
        string Email,
        string? PhoneNumber,
        DateOnly? DateOfBirth
    );

    public sealed record AddSkillsDto(List<string> Skills);

    public sealed record CandidateDetailsDto(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string? PhoneNumber,
        DateOnly? DateOfBirth,
        IReadOnlyList<SkillDto> Skills
    );

    public sealed record CandidateListItemDto(
        Guid Id,
        string FullName,
        string Email,
        IReadOnlyList<string> Skills
    );

    public sealed record SkillDto(Guid Id, string Name);
}
