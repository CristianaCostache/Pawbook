namespace Pawbook.Models
{
    public class Friendship
    {
        public int FriendshipId { get; set; }
        public int FriendId { get; set; }
        public int UserId { get; set; }
        public User? Friend { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
