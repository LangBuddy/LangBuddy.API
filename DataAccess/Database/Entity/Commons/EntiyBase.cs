namespace Database.Entity.Commons
{
    public abstract class EntityBase
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; } = null;
        public DateTime? DeleteDate { get; set; } = null;

        public void SetCreateTime()
        {
            CreateDate = DateTime.UtcNow;
        }

        public void SeDeleteTime()
        {
            DeleteDate = DateTime.UtcNow;
        }

        public void SetUpdateTime()
        {
            UpdateDate = DateTime.UtcNow;
        }

    }
}
