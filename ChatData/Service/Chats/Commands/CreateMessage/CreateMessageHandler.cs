using Database;
using Database.Common;
using Database.Data;
using Database.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Service.Chats.Commands.CreateMessage
{
    public class CreateMessageHandler : IRequestHandler<CreateMessageCommand>
    {
        private readonly DataDbContext _dataDbContext;

        private readonly ICacheService _cacheService;

        public CreateMessageHandler(DataDbContext dataDbContext, ICacheService cacheService)
        {
            _dataDbContext = dataDbContext;
            _cacheService = cacheService;
        }

        public async Task Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var chat = await _dataDbContext.ChatRooms.FirstOrDefaultAsync(
                el => el.Id == request.ChatRoomId);

            if (chat == null)
            {
                throw new ArgumentNullException(nameof(request.ChatRoomId));
            }

            var message = new Message()
            {
                Value = request.Value,
                ChatRoom = chat,
                UseId = request.UseId
            };

            await _dataDbContext.Messages.AddAsync(message);

            await _dataDbContext.SaveChangesAsync();

            await _cacheService.RemoveData(request.ChatRoomId.ToString(), CachePrefixes.Messages, cancellationToken);
        }
    }
}
