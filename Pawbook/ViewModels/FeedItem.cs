using Pawbook.Models;

namespace Pawbook.ViewModels
{
    public class FeedItem
    {
        public Post Post { get; set; }
        public User User { get; set; }

        public int PawsNumber { get; set; }
        public int CommentsNumber { get; set; }
        public bool pawed { get; set; }
    }
}
