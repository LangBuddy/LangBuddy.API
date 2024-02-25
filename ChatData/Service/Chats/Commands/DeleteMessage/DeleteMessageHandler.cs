using Database.Common;
using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Database.Data;

namespace Service.Chats.Commands.DeleteMessage
{
    public class DeleteMessageHandler : IRequestHandler<DeleteMessageCommand>
    {
        private readonly DataDbContext _dataDbContext;

        private readonly ICacheService _cacheService;

        public DeleteMessageHandler(DataDbContext dataDbContext, ICacheService cacheService)
        {
            _dataDbContext = dataDbContext;
            _cacheService = cacheService;
        }

        public async Task Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _dataDbContext.Messages
                .FirstOrDefaultAsync(el => el.Id == request.MessageId, cancellationToken);

            if (message == null)
            {
                throw new ArgumentNullException(request.MessageId.ToString());
            }

            message.SetDeleteTime();

            await _dataDbContext.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveData(message.ChatRoomId.ToString(), CachePrefixes.Messages, cancellationToken);
        }
    }
}
