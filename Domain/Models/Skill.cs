namespace Domain.Models;

public sealed class Skill
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;

    private readonly List<CandidateSkill> _candidateSkills = new();
    public IReadOnlyCollection<CandidateSkill> CandidateSkills => _candidateSkills;

    private Skill() { }

    private Skill(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Skill Create(string name)
    {
        var normalized = NormalizeName(name);
        return new Skill(Guid.NewGuid(), normalized);
    }

    public void Rename(string name)
    {
        Name = NormalizeName(name);
    }

    private static string NormalizeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Skill name is required.", nameof(name));

        var trimmed = name.Trim();
        if (trimmed.Length > 100)
            throw new ArgumentException("Skill name is too long (max 100).", nameof(name));

        return trimmed;
    }
}
