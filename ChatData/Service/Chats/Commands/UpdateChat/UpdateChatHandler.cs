using Database.Common;
using Database;
using MediatR;
using Database.Data;
using Microsoft.EntityFrameworkCore;
using Database.Entity;
using System;

namespace Service.Chats.Commands.UpdateChat
{
    public class UpdateChatHandler : IRequestHandler<UpdateChatCommand>
    {
        private readonly DataDbContext _dataDbContext;

        private readonly ICacheService _cacheService;

        public UpdateChatHandler(DataDbContext dataDbContext, ICacheService cacheService)
        {
            _dataDbContext = dataDbContext;
            _cacheService = cacheService;
        }

        public async Task Handle(UpdateChatCommand request, CancellationToken cancellationToken)
        {
            var chat = await _dataDbContext.ChatRooms
                .FirstOrDefaultAsync(el => el.Id == request.ChatId, cancellationToken);

            if(chat == null)
            {
                throw new ArgumentNullException(request.ChatId.ToString());
            }

            if(request.Title is not null)
            {
                chat.Title = request.Title;
            }


            if (request.UsersId is not null)
            {
                chat.UsersId = request.UsersId;
            }


            if(request.Title is not null || request.UsersId is not null)
            {
                chat.SetUpdateTime();

                await _dataDbContext.SaveChangesAsync(cancellationToken);

                chat.UsersId.ForEach(userId => _cacheService.RemoveData(userId.ToString(), CachePrefixes.Chats, cancellationToken));
            }
        }
    }
}
