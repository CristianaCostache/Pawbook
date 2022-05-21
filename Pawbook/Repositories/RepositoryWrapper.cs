using Pawbook.Models;
using Pawbook.Repositories.Intrfaces;

namespace Pawbook.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private PawbookContext _pawbookContext;
        private IUserRepository? _userRepository;
        private IPostRepository? _postRepository;
        private IPawRepository? _pawRepository;
        private ICommentRepository? _commentRepository;

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_pawbookContext);
                }

                return _userRepository;
            }
        }

        public IPostRepository PostRepository
        {
            get
            {
                if (_postRepository == null)
                {
                    _postRepository = new PostRepository(_pawbookContext);
                }

                return _postRepository;
            }
        }

        public IPawRepository PawRepository
        {
            get
            {
                if (_pawRepository == null)
                {
                    _pawRepository = new PawRepository(_pawbookContext);
                }

                return _pawRepository;
            }
        }

        public ICommentRepository CommentRepository
        {
            get
            {
                if (_commentRepository == null)
                {
                    _commentRepository = new CommentRepository(_pawbookContext);
                }

                return _commentRepository;
            }
        }

        public RepositoryWrapper(PawbookContext pawbookContext)
        {
            _pawbookContext = pawbookContext;
        }

        public void Save()
        {
            _pawbookContext.SaveChanges();
        }
    }
}
