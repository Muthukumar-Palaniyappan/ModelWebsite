using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SiteApi.Business.Interfaces;
using SiteApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteApi.Controllers
{
    [Route("api/[Controller]")]
    public class AuthenticationController:Controller

    {
        private readonly IOptions<SiteConfigs> _config;
        private readonly IUserService _userService;
        public AuthenticationController(IOptions<SiteConfigs> config, IUserService userService)
        {
            _config = config ?? throw new ArgumentException($"Parm is invalid - {config}");
            _userService = userService ?? throw new ArgumentException($"Parm is invalid - {userService}");
        }
        [HttpGet("Authenticate")]
        [AllowAnonymous]
        public IActionResult  Authenticate(string userName, string password)
        {
            string jwtToken;
            var secretKey = _config.Value?.AppSecret;
            if(_userService.Authenticate(userName, password, secretKey, out jwtToken))
                return Ok(jwtToken);
            return BadRequest(new { message = "Invalid Credentials" });

        }

        [HttpGet("ValidateToken")]
        [AllowAnonymous]

        public bool ValidateToken(string jwtToken)
        {
            var secretKey = _config.Value?.AppSecret;
            return _userService.Validate(jwtToken, secretKey);
            
        }

    }
}
