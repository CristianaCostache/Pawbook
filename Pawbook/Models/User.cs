using System.ComponentModel.DataAnnotations.Schema;

namespace Pawbook.Models
{
    public enum Gender
    {
        Female, Male
    }
    public enum Type
    {
        Dog, Cat, Rabbit, Otter, Fish
    }
    public class User
    {
        public User()
        {
            Posts = new List<Post>();
            Friendships = new List<Friendship>();
            Haterships = new List<Hatership>();
        }

        public int UserId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string OwnerName { get; set; }

        public Gender Gender { get; set; }

        public Type Type { get; set; }

        public string ImageName { get; set; }

        public ICollection<Post>? Posts { get; set; }

        public virtual ICollection<Friendship>? Friendships { get; set; }

        public virtual ICollection<Hatership>? Haterships { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
