using BookApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Services
{
    public class AccountService : IAccountService
    {
        public string HashPassword(string password)
        {
            //generate a salt and hash the password
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashPassword;
        }
    }
}
