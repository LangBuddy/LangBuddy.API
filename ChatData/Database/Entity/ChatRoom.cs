using Database.Entity.Common;

namespace Database.Entity
{
    public class ChatRoom: EntityBase
    {
        public string Title { get; set; }
        public List<Message> Messages { get; set; }
        public List<long> UsersId { get; set; }
    }
}
