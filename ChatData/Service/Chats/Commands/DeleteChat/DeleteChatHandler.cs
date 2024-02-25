using Database.Common;
using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Database.Data;

namespace Service.Chats.Commands.DeleteChat
{
    public class DeleteChatHandler : IRequestHandler<DeleteChatCommand>
    {
        private readonly DataDbContext _dataDbContext;

        private readonly ICacheService _cacheService;

        public DeleteChatHandler(DataDbContext dataDbContext, ICacheService cacheService)
        {
            _dataDbContext = dataDbContext;
            _cacheService = cacheService;
        }

        public async Task Handle(DeleteChatCommand request, CancellationToken cancellationToken)
        {
            var chat = await _dataDbContext.ChatRooms
                .FirstOrDefaultAsync(el => el.Id == request.ChatId, cancellationToken);

            if (chat == null)
            {
                throw new ArgumentNullException(request.ChatId.ToString());
            }

            chat.SetDeleteTime();

            await _dataDbContext.SaveChangesAsync(cancellationToken);

            chat.UsersId.ForEach(userId => _cacheService.RemoveData(userId.ToString(), CachePrefixes.Chats, cancellationToken));
        }
    }
}
