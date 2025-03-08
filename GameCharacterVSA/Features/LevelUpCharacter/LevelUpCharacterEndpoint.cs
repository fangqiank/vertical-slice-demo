using GameCharacterVSA.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static GameCharacterVSA.Features.LevelUpCharacter.LevelUpCharacter;

namespace GameCharacterVSA.Features.LevelUpCharacter
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelUpCharacterEndpoint(DataContext db, IMediator mediator) : ControllerBase
    {
        //[HttpPatch("{id}/level-up")]
        //public async Task<IActionResult> LevelUp(Guid id)
        //{
        //    var character = await db.GameCharacters.FindAsync(id);
        //    if (character == null) return NotFound();

        //    character.Level++;
        //    await db.SaveChangesAsync();

        //    return Ok(new { Message = "Character leveled up successfully!" });
        //}

        [HttpPatch("{id}/level-up")]
        public async Task<IActionResult> LevelUp(Guid id)
        {
            var success = await mediator.Send(new Command(id));

            return success ? Ok(new { Message = "Character leveled up successfully!" }) : NotFound();
        }
    }
}
