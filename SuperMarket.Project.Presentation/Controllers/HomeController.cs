using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuperMarket.Project.BusinessLogic;
using SuperMarket.Project.BusinessLogic.Concrete;
using SuperMarket.Project.Entity;
using SuperMarket.Project.Presentation.Models;
using SuperMarket.Project.Presentation.Models.CustomModel;

namespace SuperMarket.Project.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUserService userService;

        public HomeController(ILogger<HomeController> logger, IUserService _userService)
        {
            _logger = logger;
            userService = _userService;
        }

        public IActionResult Index()
        {
            var name = User.Claims.Where(c => c.Type == ClaimTypes.Name)
               .Select(c => c.Value).SingleOrDefault();
            ViewData["Name"] = name;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel, string returnUrl = null)
        {
            if (LoginUser(loginModel.Username, loginModel.Password))
            {
                User user = userService.GetByUserName(loginModel.Username);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginModel.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index", "Products");
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }

        private bool LoginUser(string username, string password)
        {
            bool result = false;
            User user = userService.GetByUserName(username);
            if (user != null && username == user.UserName && password == user.Password.ToString())
            {
                return true;
            }
            return result;
        }
    }
}
