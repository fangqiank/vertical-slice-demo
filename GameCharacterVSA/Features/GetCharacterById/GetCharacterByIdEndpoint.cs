using GameCharacterVSA.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static GameCharacterVSA.Features.GetCharacterById.GetCharacter;

namespace GameCharacterVSA.Features.GetCharacterById
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetCharacterByIdEndpoin(DataContext db, IMediator mediator) : ControllerBase
    {
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(Guid id)
        //{
        //    var character = await db.GameCharacters.FindAsync(id);
        //    return character != null ? Ok(character) : NotFound();
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var character = await mediator.Send(new Query(id));
            return character != null ? Ok(character) : NotFound();
        }

    }
}
