using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SiteCheck.Application.Users.Commands.AuthUser;
using SiteCheck.Application.Users.Commands.LoginUser;
using SiteCheck.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SiteCheck.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator) => (this.mediator) = (mediator);

        [HttpGet]
        public ViewResult Login() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel) 
        {
            if (!ModelState.IsValid) 
            {
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            var result = await mediator.Send(new LoginUserRequest {UserName = viewModel.UserName, PasswordHash = viewModel.Password });

            if (!result) 
            {
                ModelState.AddModelError("", "Пользователь не найден");
                return View(viewModel);
            }

            await Autenticate(viewModel.UserName);

            return RedirectToAction("Index", "Sites");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            var result = await mediator.Send(new RegisterUserRequest { UserName = viewModel.UserName, PasswordHash = viewModel.Password });

            if (!result)
            {
                ModelState.AddModelError("", "Пользователь уже зарегестрирован");
                return View(viewModel);
            }

            await Autenticate(viewModel.UserName);

            return RedirectToAction("Index", "Sites");
        }

        private async Task Autenticate(string userName) 
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType,userName)
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity)); 
        }
    }
}
