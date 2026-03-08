using LivelySheets.CatalogService.Application.Commands;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace LivelySheets.CatalogService.API.Contracts.Requests
{
    public class PostFindBattleDto
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }

        public static explicit operator FindBattleCommand(PostFindBattleDto p) =>
            new ()
            {
                UserId = p.UserId,
                BookId = p.BookId,
            };
    }
}
