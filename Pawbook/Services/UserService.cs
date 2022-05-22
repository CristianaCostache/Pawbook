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

        public User GetUserByEmail(string email)
        {
            User user = _repositoryWrapper.UserRepository.FindByCondition(userItem => userItem.Email == email).FirstOrDefault();
            return user;
        }

        public User GetUserById(int userId)
        {
            User user = _repositoryWrapper.UserRepository.FindByCondition(userItem => userItem.UserId == userId).FirstOrDefault();
            return user;
        }

        public List<User> GetUserByName(string searchString)
        {
            var users = new List<User>();
            users = _repositoryWrapper.UserRepository.FindByCondition(user => user.Name == searchString).ToList();
            return users;
        }

        public bool PasswordMatch(User user)
        {
            User dbUser = _repositoryWrapper.UserRepository.FindByCondition(userItem => userItem.Email == user.Email).FirstOrDefault();
            
            bool verified = BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password);
            
            if (verified)
            {
                return true;
            }
            return false;
        }

        public void Register(User user)
        {
            if (user.Email.Contains("@pawbook.com"))
            {
                user.UserRole = User.USER_ROLE_ADMIN;
            }
            else
            {
                user.UserRole = User.USER_ROLE_USER;
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = passwordHash;

            addImage(user);
            _repositoryWrapper.UserRepository.Create(user);
            _repositoryWrapper.Save();
        }

        public void Update(User user)
        {
            addImage(user);

            User dbUser = GetUserById(user.UserId);
            dbUser.ImageName = user.ImageName;

            _repositoryWrapper.UserRepository.Update(dbUser);
            _repositoryWrapper.Save();
        }

        public bool UserExist(User user)
        {
            User dbUser = _repositoryWrapper.UserRepository.FindByCondition(userItem => userItem.Email == user.Email).FirstOrDefault();
            if(dbUser != null)
            {
                return true;
            }
            return false;
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
