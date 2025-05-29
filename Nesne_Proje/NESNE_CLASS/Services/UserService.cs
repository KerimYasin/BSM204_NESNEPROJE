using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nesne_Proje.NESNE_CLASS.Models;
using Nesne_Proje.NESNE_CLASS.Repositories;


namespace Nesne_Proje.NESNE_CLASS.Services
{
    public class UserService
    {
        private readonly UserRepo _userRepo;

        public UserService(string connectionString)
        {
            _userRepo = new UserRepo(connectionString);
        }

        public Kullanıcı GetUserById(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Geçersiz kullanıcı ID");

            return _userRepo.GetUserById(userId);
        }

        public bool UpdateUserProfile(Kullanıcı user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentException("Kullanıcı adı ve email boş olamaz");

            return _userRepo.UpdateUser(user);
        }

        public bool UpdateUserPassword(int userId, string newPasswordHash)
        {
            if (userId <= 0)
                throw new ArgumentException("Geçersiz kullanıcı ID");

            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new ArgumentException("Yeni şifre boş olamaz");

            return _userRepo.UpdateUserPassword(userId, newPasswordHash);
        }

        public Kullanıcı AuthenticateUser(string username, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(passwordHash))
                return null;

            return _userRepo.GetUserByCredentials(username, passwordHash);
        }

        public bool IsUserInRole(int userId, string roleName)
        {
            if (userId <= 0 || string.IsNullOrWhiteSpace(roleName))
                return false;

            var user = _userRepo.GetUserById(userId);
            if (user == null)
                return false;

            return string.Equals(user.Role, roleName, StringComparison.OrdinalIgnoreCase);
        }

        public bool AddUser(Kullanıcı user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.PasswordHash))
                throw new ArgumentException("Kullanıcı adı ve şifre boş olamaz");

            _userRepo.AddUser(user); 
            return true; 
        }

        public bool DeleteUser(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Geçersiz kullanıcı ID");

            return _userRepo.DeleteUser(userId);
        }
    }
}
