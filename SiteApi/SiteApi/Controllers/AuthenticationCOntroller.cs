using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteApi.Controllers
{
    public class AuthenticationController:Controller

    {
        public string Authenticate(string userName, string password)
        {
            string jwtToken=null;
            return jwtToken;
        }

    }
}
