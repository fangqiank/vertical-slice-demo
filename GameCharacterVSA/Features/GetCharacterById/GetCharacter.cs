using GameCharacterVSA.Data;
using GameCharacterVSA.Entities;
using MediatR;

namespace GameCharacterVSA.Features.GetCharacterById
{
    public static class GetCharacter
    {
        public record Query(Guid Id) : IRequest<GameCharacter>;
        public class Handler(DataContext db) : IRequestHandler<Query, GameCharacter>
        {
            public async Task<GameCharacter?> Handle(
                Query request,
                CancellationToken cancellationToken
                )
            {
                return await db.GameCharacters.FindAsync(request.Id);
            }
        }
    }
}
