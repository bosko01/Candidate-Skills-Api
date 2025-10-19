using Application.DTOs;
using Application.Skills.Commands.Create;
using Application.Skills.Commands.Remove;
using Application.Skills.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SkillsController(IMediator mediator) => _mediator = mediator;

        // POST /api/skills  — kreira novu veštinu (ili vrati postojeću preko GetOrCreate)
        // Prima plain string u telu: "C#"
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string name)
        {
            var id = await _mediator.Send(new CreateSkill.Command(name));
            // Možeš da vratiš i Location ka GET /api/skills (nema GetById, pa ka listi)
            return CreatedAtAction(nameof(GetAll), new { id }, new { id, name });
        }

        // GET /api/skills  — vraća sve veštine
        [HttpGet]
        public Task<IReadOnlyList<SkillDto>> GetAll()
            => _mediator.Send(new GetAllSkills.Query());

        // DELETE /api/skills/{id}  — briše veštinu
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new RemoveSkill.Command(id));
            return NoContent();
        }
    }
}
