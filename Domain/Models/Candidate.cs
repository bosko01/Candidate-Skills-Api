using Domain.ValueObjects;
using Exceptions; 
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Models;

public sealed class Candidate
{
    public Guid Id { get; private set; }

    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public Email Email { get; private set; } = default!;

    public string? PhoneNumber { get; private set; }
    public DateOnly? DateOfBirth { get; private set; }

    private readonly List<CandidateSkill> _candidateSkills = new();
    public IReadOnlyCollection<CandidateSkill> CandidateSkills => _candidateSkills;

    private Candidate() { }

    private Candidate(Guid id, string firstName, string lastName, Email email, string? phoneNumber, DateOnly? dateOfBirth)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
    }

    public static Candidate Create(string firstName, string lastName, string email, string? phoneNumber, DateOnly? dateOfBirth = null)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new BussinessRuleValidationExeption("First name is required.");
        if (string.IsNullOrWhiteSpace(lastName))
            throw new BussinessRuleValidationExeption("Last name is required.");

        var emailVo = Email.Create(email ?? string.Empty);

        return new Candidate(
            id: Guid.NewGuid(),
            firstName: firstName.Trim(),
            lastName: lastName.Trim(),
            email: emailVo,
            phoneNumber: string.IsNullOrWhiteSpace(phoneNumber) ? null : phoneNumber.Trim(),
            dateOfBirth: dateOfBirth
        );
    }

    public void AddSkill(Skill skill)
    {
        if (skill is null) throw new ArgumentNullException(nameof(skill));
        if (HasSkill(skill.Id))
            throw new BussinessRuleValidationExeption("Candidate already has this skill.");

        var link = CandidateSkill.Create(this, skill);
        _candidateSkills.Add(link);
    }

    public void RemoveSkill(Skill skill)
    {
        if (skill is null) throw new ArgumentNullException(nameof(skill));

        var existing = _candidateSkills.FirstOrDefault(cs => cs.Skill.Id == skill.Id);
        if (existing is null)
            throw new BussinessRuleValidationExeption("Candidate does not have this skill.");

        _candidateSkills.Remove(existing);
    }

    public void ChangeEmail(string newEmail) => Email = Email.Create(newEmail);
    public void ChangePhone(string? phone) => PhoneNumber = string.IsNullOrWhiteSpace(phone) ? null : phone.Trim();
    public void ChangeName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new BussinessRuleValidationExeption("First and last name are required.");
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
    }

    private bool HasSkill(Guid skillId) => _candidateSkills.Any(cs => cs.Skill.Id == skillId);

    public static Candidate Create(string firstName, string lastName, string email, string phoneNumber, DateOnly? dateOfBirth, List<string> skills)
    {
        throw new NotImplementedException();
    }
}
