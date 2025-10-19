namespace Domain.Models;

public sealed class CandidateSkill
{
    public Guid CandidateId { get; private set; }
    public Candidate Candidate { get; private set; } = default!;

    public Guid SkillId { get; private set; }
    public Skill Skill { get; private set; } = default!;

    public DateTime CreatedAt { get; private set; }

    private CandidateSkill() { }

    private CandidateSkill(Candidate candidate, Skill skill)
    {
        Candidate = candidate ?? throw new ArgumentNullException(nameof(candidate));
        Skill = skill ?? throw new ArgumentNullException(nameof(skill));

        CandidateId = candidate.Id;
        SkillId = skill.Id;
        CreatedAt = DateTime.UtcNow;
    }

    public static CandidateSkill Create(Candidate candidate, Skill skill)
        => new(candidate, skill);
}
