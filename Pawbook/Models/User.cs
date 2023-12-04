using System.ComponentModel.DataAnnotations;
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

        public const string USER_ROLE_ADMIN = "Admin";
        public const string USER_ROLE_USER = "User";
        public int UserId { get; set; }

        public string? Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string? Password { get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, type again!")]
        public string? ConfirmPassword { get; set; }
        public string? UserRole { get; set; }

        public string? Name { get; set; }

        public string? OwnerName { get; set; }

        public Gender Gender { get; set; }

        public Type Type { get; set; }

        public string? ImageName { get; set; }

        public ICollection<Post>? Posts { get; set; }

        public virtual ICollection<Friendship>? Friendships { get; set; }

        public virtual ICollection<Hatership>? Haterships { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
