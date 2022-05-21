﻿using Pawbook.Models;
using Pawbook.Repositories.Intrfaces;
using Pawbook.Services.Interfaces;
using Pawbook.ViewModels;

namespace Pawbook.Services
{
    public class FeedItemService : IFeedItemService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IPostService _postService;
        private IPawService _pawService;
        private ICommentService _commentService;

        public FeedItemService(IRepositoryWrapper repositoryWrapper, IPostService postService, IPawService pawService, ICommentService commentService)
        {
            _repositoryWrapper = repositoryWrapper;
            _postService = postService;
            _pawService = pawService;
            _commentService = commentService;
        }

        public List<FeedItem> GetAll()
        {
            User user = _repositoryWrapper.UserRepository.FindAll().FirstOrDefault();

            List<FeedItem> feedItems = new List<FeedItem>();
            List<Post> posts = _repositoryWrapper.PostRepository.FindAll().ToList();
            posts.Reverse();
            foreach (Post post in posts)
            {
                FeedItem feedItem = new FeedItem();
                feedItem.Post = post;
                feedItem.User = _repositoryWrapper.UserRepository.FindByCondition(user => user.UserId == post.UserId).FirstOrDefault();
                feedItem.PawsNumber = _pawService.CountPawsByPostId(post.PostId);
                feedItem.CommentsNumber = _commentService.CountCommentsByPostId(post.PostId);
                feedItem.pawed = _pawService.IsPawedByUser(post.PostId, user.UserId);
                feedItems.Add(feedItem);
            }
            return feedItems;
        }

        public List<FeedItem> GetByUser(int userId, int isLoggedUser = 0)
        {
            User user = new User();
            if (isLoggedUser == 1)
            {
                user = _repositoryWrapper.UserRepository.FindAll().FirstOrDefault();
            }
            else
            {
                user = _repositoryWrapper.UserRepository.FindByCondition(user =>user.UserId == userId).FirstOrDefault();
            }
            List<Post> posts = _postService.GetByUserId(user.UserId);
            posts.Reverse();
            List<FeedItem> feedItems = new List<FeedItem>();
            FeedItem feedItem = new FeedItem();
            feedItem.User = user;
            feedItems.Add(feedItem);
            foreach (Post post in posts)
            {
                feedItem = new FeedItem();
                feedItem.Post = post;
                feedItem.User = user;
                feedItem.PawsNumber = _pawService.CountPawsByPostId(post.PostId);
                feedItem.CommentsNumber = _commentService.CountCommentsByPostId(post.PostId);
                feedItem.pawed = _pawService.IsPawedByUser(post.PostId, userId);
                feedItems.Add(feedItem);
            }
            return feedItems;
        }
    }
}
