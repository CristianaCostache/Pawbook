using Pawbook.Models;
using Pawbook.Repositories.Intrfaces;
using Pawbook.Services.Interfaces;

namespace Pawbook.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserService(IRepositoryWrapper repositoryWrapper, IWebHostEnvironment webHostEnvironment)
        {
            _repositoryWrapper = repositoryWrapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public User GetUserByEmail(string email)
        {
            return _repositoryWrapper.UserRepository.FindByCondition(userItem => userItem.Email == email).First();
        }

        public User GetUserById(int userId)
        {
            return _repositoryWrapper.UserRepository.FindByCondition(userItem => userItem.UserId == userId).First();
        }

        public List<User> GetUserByName(string searchString)
        {
            return _repositoryWrapper.UserRepository.FindByCondition(user => user.Name == searchString).ToList();
        }

        public bool PasswordMatch(User user)
        {
            User dbUser = _repositoryWrapper.UserRepository.FindByCondition(userItem => userItem.Email == user.Email).First();
            
            bool verified = BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password);

            return verified;
        }

        public void Register(User user)
        {
            user.UserRole = user.Email!.Contains("@pawbook.com") ? User.USER_ROLE_ADMIN : User.USER_ROLE_USER;
            if (user.Password != null)
            {
                user.Password = HashPassword(user.Password);
            }

            addImage(user);
            _repositoryWrapper.UserRepository.Create(user);
            _repositoryWrapper.Save();
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public void Update(User user)
        {
            addImage(user);

            User dbUser = GetUserById(user.UserId);
            dbUser.ImageName = user.ImageName;
            UpdateDbUser(dbUser);
        }

        private void UpdateDbUser(User dbUser)
        {
            _repositoryWrapper.UserRepository.Update(dbUser);
            _repositoryWrapper.Save();
        }

        public bool UserExist(User user)
        {
            User dbUser = _repositoryWrapper.UserRepository.FindByCondition(userItem => userItem.Email == user.Email).FirstOrDefault()!;
            
            return dbUser != null;
        }

        private void addImage(User user)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(user.ImageFile!.FileName);
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
