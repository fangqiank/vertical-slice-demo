using GameCharacterVSA.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static GameCharacterVSA.Features.DeleteCharacter.DeleteCharacter;

namespace GameCharacterVSA.Features.DeleteCharacter
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteCharacterEndpoint(DataContext db, IMediator mediator) : ControllerBase
    {
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var character = await db.GameCharacters.FindAsync(id);
        //    if (character == null) return NotFound();

        //    db.GameCharacters.Remove(character);
        //    await db.SaveChangesAsync();

        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await mediator.Send(new Command(id));

            return success ? NoContent() : NotFound();
        }
    }
}
