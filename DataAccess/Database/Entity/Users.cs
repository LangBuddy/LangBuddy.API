using Database.Entity.Commons;
using Database.Mocks;

namespace Database.Entity
{
    public class Users: EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderList Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public Accounts Account { get; set; }
    }
}
