using Pawbook.Models;
using Pawbook.Repositories.Intrfaces;
using Pawbook.Services.Interfaces;

namespace Pawbook.Services
{
    public class UserService : IUserService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IWebHostEnvironment _webHostEnvironment;

        public UserService(IRepositoryWrapper repositoryWrapper, IWebHostEnvironment webHostEnvironment)
        {
            _repositoryWrapper = repositoryWrapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public List<User> GetUserByName(string searchString)
        {
            var users = new List<User>();
            users = _repositoryWrapper.UserRepository.FindByCondition(user => user.Name == searchString).ToList();
            return users;
        }

        public void Update(User user)
        {
            addImage(user);
            _repositoryWrapper.UserRepository.Update(user);
            _repositoryWrapper.Save();
        }

        private void addImage(User user)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
            string extension = Path.GetExtension(user.ImageFile.FileName);
            user.ImageName = fileName = fileName + DateTime.Now.ToString("_yyMMddHHmmss") + extension;
            string path = Path.Combine(wwwRootPath + "/img/profile/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                user.ImageFile.CopyTo(fileStream);
            }
        }
    }
}
