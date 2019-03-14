using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteApi.Business.Interfaces
{
    public interface IUserService
    {
        bool Authenticate(string userName, string password,string secretKey, out string jwtToken);
        bool Validate(string jwtToken,string secretkey);
    }
}
