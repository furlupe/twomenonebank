﻿using System.Reflection;
using Bank.Auth.Common.Attributes;
using Bank.Auth.Http.AuthClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace Bank.Auth.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : Controller
    {
        private readonly AuthClient _authClient;

        public VersionController(AuthClient authClient)
        {
            _authClient = authClient;
        }

        [HttpGet]
        public string Version() =>
            typeof(VersionController)
                .Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion ?? "";

        [HttpGet("authenticated")]
        [Authorize(
            AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme
        )]
        [CalledByHuman]
        public string VersionAuthenticated() => Version();

        [HttpGet("authenticated-service")]
        [Authorize(
            AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme
        )]
        [CalledByService]
        public string VersionAuthenticatedForService() => Version();

        [HttpGet("through-service")]
        public Task<string> VersionThroughtService() => _authClient.Version();

        [HttpGet("view")]
        public IActionResult Index() => View((object)Version());
    }
}
