namespace Pawbook.Models
{
    public class Hatership
    {
        public int HatershipId { get; set; }
        public string? Reason { get; set; }
        public int FriendId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
