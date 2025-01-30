namespace Domain.Entities
{
    public class Account : BaseEntity
    {
        public float Balance { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
