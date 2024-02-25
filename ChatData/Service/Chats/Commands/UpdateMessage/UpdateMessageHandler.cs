using Database.Common;
using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Database.Data;
using System;

namespace Service.Chats.Commands.UpdateMessage
{
    public class UpdateMessageHandler : IRequestHandler<UpdateMessageCommand>
    {
        private readonly DataDbContext _dataDbContext;

        private readonly ICacheService _cacheService;

        public UpdateMessageHandler(DataDbContext dataDbContext, ICacheService cacheService)
        {
            _dataDbContext = dataDbContext;
            _cacheService = cacheService;
        }

        public async Task Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _dataDbContext.Messages
                .FirstOrDefaultAsync(el => el.Id == request.MessageId, cancellationToken);

            if (message == null)
            {
                throw new ArgumentNullException(request.MessageId.ToString());
            }

            message.Value = request.Value;
            message.SetUpdateTime();

            await _dataDbContext.SaveChangesAsync();
            await _cacheService.RemoveData(message.ChatRoomId.ToString(), CachePrefixes.Messages, cancellationToken);
        }
    }
}
