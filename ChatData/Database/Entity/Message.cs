using Database.Entity.Common;

namespace Database.Entity
{
    public class Message : EntityBase
    {
        public string Value { get; set; }
        public long ChatRoomId { get; set; }
        public long UserId { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }
}
