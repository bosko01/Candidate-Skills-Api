using Application.Candidates.Commands.AddSkills;
using Application.Candidates.Commands.Create;
using Application.Candidates.Commands.Remove;
using Application.Candidates.Commands.RemoveSkill;
using Application.Candidates.Queries.GetAll;
using Application.Candidates.Queries.GetById;
using Application.Candidates.Queries.Search;
using Application.Commands.Create;
using Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class CandidatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpGet("all")]
        public async Task<ActionResult<IReadOnlyList<CandidateListItemDto>>> ReadAll()
        {
            var result = await _mediator.Send(new GetAllCandidates.Query());
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public Task<CandidateDetailsDto?> GetById(Guid id)
    => _mediator.Send(new GetCandidateById.Query(id));

        [HttpGet]
        public Task<IReadOnlyList<CandidateListItemDto>> Search([FromQuery] string? name, [FromQuery] string? skills)
            => _mediator.Send(new SearchCandidates.Query(name, skills));

        [HttpPost]
        public Task<CandidateDetailsDto> Create([FromBody] CreateCandidateDto dto)
            => _mediator.Send(new CreateCandidate.Command(dto));

        [HttpPost("{id:guid}/skills")]
        public Task AddSkills(Guid id, [FromBody] AddSkillsDto dto)
            => _mediator.Send(new AddSkillsToCandidate.Command(id, dto));

        [HttpDelete("{id:guid}/skills/{skillId:guid}")]
        public Task RemoveSkill(Guid id, Guid skillId)
            => _mediator.Send(new RemoveSkillFromCandidate.Command(id, skillId));

        [HttpDelete("{id:guid}")]
        public Task Delete(Guid id)
            => _mediator.Send(new RemoveCandidate.Command(id));

    }
}
