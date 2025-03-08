using GameCharacterVSA.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static GameCharacterVSA.Features.GetAllCharacters.GetCharacters;

namespace GameCharacterVSA.Features.GetAllCharacters
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAllCharactersEndpoint(
        DataContext db,
        IMediator mediator
        ) : ControllerBase
    {
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var characters = await _dbContext.GameCharacters.ToListAsync();
        //    return Ok(characters);
        //}

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var characters = await mediator.Send(new Query());

            return Ok(characters);
        }
    }
}
