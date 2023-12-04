using Pawbook.Models;
using Pawbook.Repositories.Intrfaces;
using Pawbook.Services.Interfaces;
using Pawbook.ViewModels;

namespace Pawbook.Services
{
    public class FeedItemService : IFeedItemService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IPostService _postService;
        private readonly IPawService _pawService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly IFriendshipService _friendshipService;

        public FeedItemService(IRepositoryWrapper repositoryWrapper, IPostService postService, 
            IPawService pawService, ICommentService commentService, IUserService userService,
            IFriendshipService friendshipService)
        {
            _repositoryWrapper = repositoryWrapper;
            _postService = postService;
            _pawService = pawService;
            _commentService = commentService;
            _userService = userService;
            _friendshipService = friendshipService;
        }

        public List<FeedItem> GetAll(int? loggedInUserId)
        {
            User user = _userService.GetUserById((int)loggedInUserId!);


            List<Post> posts = _repositoryWrapper.PostRepository.FindByCondition(postItem => postItem.Status == Post.POST_STATUS_AVAILABLE).ToList();
            posts.Reverse();
            List<FeedItem> feedItems = BuildFeedItems(user, posts);

            return feedItems;
        }

        private List<FeedItem> BuildFeedItems(User user, List<Post> posts)
        {
            List<FeedItem> feedItems = new List<FeedItem>();
            foreach (Post post in posts)
            {
                FeedItem feedItem = BuildFeedITem(user, post);
                feedItems.Add(feedItem);
            }

            return feedItems;
        }

        private FeedItem BuildFeedITem(User user, Post post)
        {
            FeedItem feedItem = new FeedItem();
            feedItem.Post = post;
            feedItem.User = _repositoryWrapper.UserRepository.FindByCondition(user => user.UserId == post.UserId).FirstOrDefault();
            feedItem.PawsNumber = _pawService.CountPawsByPostId(post.PostId);
            feedItem.CommentsNumber = _commentService.CountCommentsByPostId(post.PostId);
            feedItem.pawed = _pawService.IsPawedByUser(post.PostId, user.UserId);

            return feedItem;
        }

        public List<FeedItem> GetByUser(int userId, int? loggedInUserId, int isLoggedUser)
        {
            User user;

            if (isLoggedUser == 1)
            {
                user = _userService.GetUserById((int)loggedInUserId!);
            }
            else
            {
                user = _userService.GetUserById(userId);
            }

            List<Post> posts = _postService.GetByUserId(user.UserId);
            posts.Reverse();

            List<FeedItem> feedItems = BuildFeedItems(user, posts);
            FeedItem feedItem = new FeedItem();
            feedItem.User = user;
            feedItem.alreadyFriends = _friendshipService.IsFriendWith((int)loggedInUserId!, userId);
            feedItems.Add(feedItem);

            return feedItems;
        }
    }
}
