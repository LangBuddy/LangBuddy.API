using MediatR;
using Models.Responses;

namespace Service.Chats.Queries.GetMessages
{
    public record GetMessagesQuery(long ChatId): IRequest<List<GetMessagesResponse>>;
}
