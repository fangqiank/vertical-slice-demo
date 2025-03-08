using GameCharacterVSA.Data;
using MediatR;

namespace GameCharacterVSA.Features.DeleteCharacter
{
    public static class DeleteCharacter
    {
        public record Command(Guid Id) : IRequest<bool>;
        public class Handler(DataContext db) : IRequestHandler<Command, bool>
        {
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var character = await db.GameCharacters.FindAsync(request.Id);
                if (character == null) return false;

                db.GameCharacters.Remove(character);
                await db.SaveChangesAsync(cancellationToken);

                return true;
            }
        }
    }
}
