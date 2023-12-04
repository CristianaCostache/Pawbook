namespace Pawbook.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? Body { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public int PostId { get; set; }
        public User? User { get; set; }
    }
}
