using SiteApi.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiteApi.Data.Services
{
    public class UserRepository : IUserRepository
    {
        public bool ValidateCredentials(string username, string password)
        {
            return (username.Equals("testuser", StringComparison.OrdinalIgnoreCase)
                && password.Equals("password"));
        }
    }
}
