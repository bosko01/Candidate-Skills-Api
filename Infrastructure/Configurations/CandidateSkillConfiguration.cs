using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public sealed class CandidateSkillConfiguration : IEntityTypeConfiguration<CandidateSkill>
{
    public void Configure(EntityTypeBuilder<CandidateSkill> b)
    {
        //  (CandidateId, SkillId)
        b.HasKey(cs => new { cs.CandidateId, cs.SkillId });

        b.Property(cs => cs.CreatedAt)
         .HasDefaultValueSql("GETUTCDATE()");
    }
}
