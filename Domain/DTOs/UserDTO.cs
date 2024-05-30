using Domain.Constants;
using Domain.Entities;
using Domain.Models.Requests;
using Domain.Utils;

namespace Domain.DTOs
{
    public class UserDto
    {
        public ulong? Id { get; init; }
        public string Username { get; }
        public string Email { get; }
        public string Password { get; }
        public string Salt { get; }

        public UserDto(User user)
        {
            this.Id = user.Id;
            this.Username = user.Username;
            this.Email = user.Email;
            this.Password = user.Password;
            this.Salt = user.Salt;
        }

        public UserDto(string username, string email, string password, string salt)
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.Salt = salt;
        }

        public UserDto(CreateUserRequest request)
        {
            var size = Configuration.GetConfigurationValue<uint>(ConfigurationConstants.USER_SALT_SIZE);
            var salt = Cryptography.GenerateSalt(size);
            var password = Cryptography.EncryptPassword(request.Password, salt);
            
            this.Username = request.Username;
            this.Email = request.Email;
            this.Password = password;
            this.Salt = salt;
        }

        public bool ValidatePassword(string password)
        {
            var encryptedPassword = Cryptography.EncryptPassword(password, this.Salt);
            return encryptedPassword.Equals(this.Password);
        }

        protected bool Equals(UserDto other)
        {
            return this.Username == other.Username && this.Email == other.Email;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((UserDto)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Username, this.Email, this.Password, this.Salt);
        }

        public static bool operator ==(UserDto? left, UserDto? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UserDto? left, UserDto? right)
        {
            return !Equals(left, right);
        }
    }
}