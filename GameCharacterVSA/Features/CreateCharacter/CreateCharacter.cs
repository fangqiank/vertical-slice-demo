using GameCharacterVSA.Data;
using GameCharacterVSA.Entities;
using MediatR;

namespace GameCharacterVSA.Features.CreateCharacter
{
    public static class CreateCharacter
    {
        public record Command(string Name, string Class): IRequest<GameCharacter>;

        public class Handler(DataContext db) : IRequestHandler<Command, GameCharacter>
        {
            public async Task<GameCharacter> Handle(
                Command request, 
                CancellationToken cancellationToken
                )
            {
                var character = new GameCharacter
                {
                    Name = request.Name,
                    Class = request.Class,
                    Level = 1
                };

                db.GameCharacters.Add(character);
                await db.SaveChangesAsync();
                
                return character;
            }
        }
    }
}
