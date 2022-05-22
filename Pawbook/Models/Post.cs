using System.ComponentModel.DataAnnotations.Schema;

namespace Pawbook.Models
{
    public class Post
    {
        public Post()
        {
            Paws = new List<Paw>();
            Comments = new List<Comment>();
        }

        public const string POST_STATUS_AVAILABLE = "available";
        public const string POST_STATUS_DELETED = "deleted";
        public int PostId { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Paw>? Paws { get; set; }
        
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string Status { get; set; }
    }
}
