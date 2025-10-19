using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<CandidateSkill> CandidateSkills => Set<CandidateSkill>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
