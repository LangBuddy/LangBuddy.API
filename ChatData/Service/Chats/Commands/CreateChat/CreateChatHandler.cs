using Database;
using Database.Common;
using Database.Data;
using Database.Entity;
using MediatR;

namespace Service.Chats.Commands.CreateChat
{
    public class CreateChatHandler : IRequestHandler<CreateChatCommand>
    {

        private readonly DataDbContext _dataDbContext;

        private readonly ICacheService _cacheService;

        public CreateChatHandler(DataDbContext dataDbContext, ICacheService cacheService)
        {
            _dataDbContext = dataDbContext;
            _cacheService = cacheService;
        }

        public async Task Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var chat = new ChatRoom()
            {
                Title = request.Title,
                UsersId = request.UsersId,
            };

            chat.SetCreateTime();

            await _dataDbContext.ChatRooms.AddAsync(chat, cancellationToken);

            await _dataDbContext.SaveChangesAsync(cancellationToken);

            chat.UsersId.ForEach(userId => _cacheService.RemoveData(userId.ToString(), CachePrefixes.Chats, cancellationToken));
        }
    }
}
