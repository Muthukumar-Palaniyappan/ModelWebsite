using System;
using System.Collections.Generic;
using System.Text;

namespace SiteApi.Data.Interfaces
{
    public interface IUserRepository
    {
        bool ValidateCredentials(string username, string password);
    }
}
