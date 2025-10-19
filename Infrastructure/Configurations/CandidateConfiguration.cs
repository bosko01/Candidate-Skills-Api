using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public sealed class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> b)
    {
        b.HasKey(x => x.Id);

        b.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
        b.Property(x => x.LastName).IsRequired().HasMaxLength(100);

        // Email VO 
        b.OwnsOne(x => x.Email, email =>
        {
            email.Property(p => p.mailAddress)
                 .HasColumnName("Email")
                 .IsRequired()
                 .HasMaxLength(255);
        });

        //  CandidateSkills (1:N)
        b.HasMany(c => c.CandidateSkills)
        .WithOne(cs => cs.Candidate)
        .HasForeignKey(cs => cs.CandidateId);
    }
}
