using Application.DTOs;
using Domain.Interfaces.ICandidateRepository;
using Domain.Interfaces.UnitOfWork;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Create
{
    public static class CreateCandidateuseCase
    {
        public class Request : IRequest<Response>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string? PhoneNumber { get; set; }
            public DateOnly? DateOfBirth { get; set; }
            public List<string>? Skills { get; set; }
        }

        public class Response
        {
            public CandidateDetailsDto Candidate { get; set; }
        }

        public class UseCase : IRequestHandler<Request, Response>
        {
            private readonly ICandidateRepository _candidateRepository;
            private readonly IUnitOfWork _unitOfWork;

            public UseCase(ICandidateRepository candidateRepository, IUnitOfWork unitOfWork)
            {
                _candidateRepository = candidateRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                
                var phone = request.PhoneNumber ?? string.Empty;
                var skills = request.Skills ?? new List<string>();

                var candidate = Candidate.Create(
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    phone,
                    request.DateOfBirth,
                    skills
                );

                await _candidateRepository.AddAsync(candidate);

                await _unitOfWork.SaveChangesAsync();

                
                var candidateDetailsDto = new CandidateDetailsDto(
                    candidate.Id,
                    candidate.FirstName,
                    candidate.LastName,
                    candidate.Email.ToString(),
                    candidate.PhoneNumber,
                    candidate.DateOfBirth,
                    new List<SkillDto>() 
                );

                return new Response
                {
                    Candidate = candidateDetailsDto
                };
            }
        }
    }
}
