 using Application.DTOs;
using Application.DTOs.Mappers;
using Application.Validators;
using CQRS.ICommand;
using Domain.Interfaces.ICandidateRepository;
using Domain.Interfaces.UnitOfWork;
using Domain.Models;
using Exceptions;
using MediatR;

namespace Application.Candidates.Commands.Create;

public static class CreateCandidate
{
    public sealed record Command(CreateCandidateDto Dto) : ICommand<CandidateDetailsDto>;

    public sealed class Handler(ICandidateRepository repo, IUnitOfWork uow)
        : IRequestHandler<Command, CandidateDetailsDto>
    {
        public async Task<CandidateDetailsDto> Handle(Command c, CancellationToken ct)
        {

            var d = c.Dto;

            if (await repo.EmailExistsAsync(d.Email))
                throw new BussinessRuleValidationExeption($"Candidate with email {d.Email} already exists.");

            var normalizedPhone = PhoneHelper.NormalizeToE164(d.PhoneNumber, "RS");
            var entity = Candidate.Create(d.FirstName, d.LastName, d.Email, normalizedPhone, d.DateOfBirth);

            await repo.AddAsync(entity);
            

            await uow.SaveChangesAsync();
            return entity.ToDetailsDto();
        }
    }
}
