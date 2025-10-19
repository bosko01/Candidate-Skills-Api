using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public sealed class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> b)
    {
        b.ToTable("Skills");

        b.HasKey(x => x.Id);
        b.Property(x => x.Id)
         .ValueGeneratedNever();

        b.Property(x => x.Name)
         .IsRequired()
         .HasMaxLength(100);

        b.HasIndex(x => x.Name).IsUnique();

        b.HasMany(s => s.CandidateSkills)
        .WithOne(cs => cs.Skill)
        .HasForeignKey(cs => cs.SkillId);
    }
}
