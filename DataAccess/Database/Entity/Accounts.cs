using Database.Entity.Commons;

namespace Database.Entity
{
    public class Accounts: EntityBase
    {
        public string Nickname { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
    }
}
