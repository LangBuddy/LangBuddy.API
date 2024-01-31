using Database;
using Database.Common;
using Database.Data;
using Database.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Service.Chats.Queries.GetChatsByUserId
{
    public class GetChatsByUserIdHandler : IRequestHandler<GetChatsByUserIdQuery, List<ChatRoom>>
    {
        private readonly DataDbContext _dataDbContext;
        private readonly ICacheService _cacheService;

        public GetChatsByUserIdHandler(DataDbContext dataDbContext, ICacheService cacheService)
        {
            _dataDbContext = dataDbContext;
            _cacheService = cacheService;
        }

        public async Task<List<ChatRoom>> Handle(GetChatsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var chatsCache = await _cacheService.GetData<List<ChatRoom>>
                (request.UserId.ToString(), CachePrefixes.Chats, cancellationToken);

            if (chatsCache is not null)
            {
                return chatsCache;
            }

            var chats = await _dataDbContext.ChatRooms
                .Where(el => el.UsersId.Contains(request.UserId))
                .ToListAsync(cancellationToken);

            await _cacheService.SetData<List<ChatRoom>>(request.UserId.ToString(), CachePrefixes.Chats, chats, cancellationToken);

            return chats;

        }
    }
}
