using GameCharacterVSA.Data;
using GameCharacterVSA.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static GameCharacterVSA.Features.CreateCharacter.CreateCharacter;

namespace GameCharacterVSA.Features.CreateCharacter
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateCharacterEndpoint(DataContext db, IMediator mediator) : ControllerBase
    {
        /*
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCharacterRequest request)
        {
            var character = new GameCharacter
            {
                Name = request.Name,
                Class = request.Class,
                Level = 1
            };

            db.GameCharacters.Add(character);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(Create), new { id = character.Id }, character);
        }
        */

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Command command)
        {
            var character = await mediator.Send(command);

            return CreatedAtAction(nameof(Create), new { id = character.Id }, character);
        }
    }
}
