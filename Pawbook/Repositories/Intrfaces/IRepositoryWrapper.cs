namespace Pawbook.Repositories.Intrfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository UserRepository { get; }
        IPostRepository PostRepository { get; }
        IPawRepository PawRepository { get; }
        ICommentRepository CommentRepository { get; }
        void Save();
    }
}
