﻿using Domain.Constants;
using Domain.Entities;
using Domain.Models.Requests;
using Domain.Utils;

namespace Domain.DTOs
{
    public class UserDto
    {
        public ulong? Id { get; init; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public string? Salt { get; }

        public UserDto(User user)
        {
            this.Id = user.Id;
            this.Username = user.Username;
            this.Email = user.Email;
            this.Password = user.Password;
            this.Balance = user.Balance;
            this.Salt = user.Salt;
        }

        public UserDto(string username, string email, string password, decimal balance, string salt)
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.Balance = balance;
            this.Salt = salt;
        }

        public UserDto(UserRequest request)
        {
            var size = Configuration.GetConfigurationValue<uint>(ConfigurationConstants.USER_SALT_SIZE);
            var balance = Configuration.GetConfigurationValue<decimal>(ConfigurationConstants.BALANCE);
            //TODO: Criar a configuração do balance usando o salt como exemplo
            this.Salt = Cryptography.GenerateSalt(size);
            this.Balance = balance;
            var password = Cryptography.EncryptPassword(request.Password, this.Salt);
            
            this.Username = request.Username;
            this.Email = request.Email;
            this.Password = password;
        }

        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(this.Salt)) return false;
            var encryptedPassword = Cryptography.EncryptPassword(password, this.Salt);
            return encryptedPassword.Equals(this.Password);
        }
        public bool ValidateUser(string user)
        {
            return (this.Username == user || this.Email == user);
        }
        protected bool Equals(UserDto other)
        {
            return this.Username == other.Username && this.Email == other.Email && this.Id == other.Id;
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