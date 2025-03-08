using GameCharacterVSA.Data;
using GameCharacterVSA.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameCharacterVSA.Features.GetAllCharacters
{
    public static class GetCharacters
    {
        public record Query : IRequest<IEnumerable<GameCharacter>>;

        public class Handler(DataContext db) : IRequestHandler<Query, IEnumerable<GameCharacter>>
        {
            public async Task<IEnumerable<GameCharacter>> Handle(
                Query request,
                CancellationToken cancellationToken
                )
            {
                return await db.GameCharacters.ToListAsync();
            }
        }
    }
}
