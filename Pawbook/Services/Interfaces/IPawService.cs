using Pawbook.Models;

namespace Pawbook.Services.Interfaces
{
    public interface IPawService
    {
        List<Paw> GetPawsByPostId(int postId);
        void AddPaw(int postId);
        int CountPawsByPostId(int postId);
        bool IsPawedByUser(int postId, int userId);
    }
}
