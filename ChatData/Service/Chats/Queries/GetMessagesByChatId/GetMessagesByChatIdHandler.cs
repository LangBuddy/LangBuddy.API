﻿using Database;
using Database.Common;
using Database.Data;
using Database.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace Service.Chats.Queries.GetMessagesByChatId
{
    public class GetMessagesByChatIdHandler : IRequestHandler<GetMessagesByChatIdQuery, List<Message>>
    {
        private readonly DataDbContext _dataDbContext;

        private readonly ICacheService _cacheService;

        public GetMessagesByChatIdHandler(DataDbContext dataDbContext, ICacheService cacheService)
        {
            _dataDbContext = dataDbContext;
            _cacheService = cacheService;
        }

        public async Task<List<Message>> Handle(GetMessagesByChatIdQuery request, CancellationToken cancellationToken)
        {
            var messagesCache = await _cacheService.GetData<List<Message>>
                (request.ChatRoomId.ToString(), CachePrefixes.Messages, cancellationToken);

            if (messagesCache is not null)
            {
                return messagesCache;
            }

            var messages = await _dataDbContext.Messages
                .Where(el => el.ChatRoomId == request.ChatRoomId)
                .ToListAsync(cancellationToken);

            await _cacheService.SetData<List<Message>>(request.ChatRoomId.ToString(), CachePrefixes.Messages, messages, cancellationToken);


            return messages;
        }
    }
}
