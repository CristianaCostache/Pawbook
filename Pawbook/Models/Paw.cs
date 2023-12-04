namespace Pawbook.Models
{
    public class Paw
    {
        public int PawId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public User? User { get; set; }
    }
}
