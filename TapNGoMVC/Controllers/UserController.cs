using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TapNGo.DAL.Services.UserService;
using TapNGoMVC.ViewModels;
using TapNGo.DAL.Models;
using TapNGo.DAL.Security;

namespace TapNGoMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IMapper mapper,IConfiguration configuration)
        {
            _configuration = configuration;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Login(UserLoginVM loginVM)
        {
            try
            {
                var genericLoginFail = "Incorrect username or password";

                var existingUser = _userService.GetAllUsers()
                    .FirstOrDefault(x => x.Username == loginVM.Username);

                if (existingUser == null)
                {
                    ModelState.AddModelError("", genericLoginFail);
                    return View();
                }

                var b64hash = PasswordHashProvider.GetHash(loginVM.Password, existingUser.PwdSalt);
                if (b64hash != existingUser.PwdHash)
                {
                    ModelState.AddModelError("", genericLoginFail);
                    return View();
                }

                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.Name, loginVM.Username),
                    new Claim("FirstName", existingUser.FirstName),
                    new Claim("LastName", existingUser.LastName),
                    new Claim(ClaimTypes.Role, existingUser.Role.Name)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties();

                Task.Run(async () =>
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties)
                ).GetAwaiter().GetResult();

                if (loginVM.ReturnUrl != null)
                    return LocalRedirect(loginVM.ReturnUrl);

                return RedirectToAction("Index", "AdminOrder");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: UserController/Login
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            UserLoginVM loginVM = new UserLoginVM
            {
                ReturnUrl = returnUrl
            };

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Menu");
        }

        // GET: UserController/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: UserController/Register
        [HttpPost]
        public IActionResult Register(UserRegisterVM userVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                if(userVM.EntryPassword != _configuration["EntryPassword"])
                {
                    ModelState.AddModelError("", "Invalid entry password");
                    return View();
                }

                var trimmedUsername = userVM.Username.Trim();
                if (_userService.GetAllUsers().Any(x => x.Username.Equals(trimmedUsername)))
                {
                    ModelState.AddModelError("", "Username already exists");
                    return View();
                }
                var b64salt = PasswordHashProvider.GetSalt();
                var b64hash = PasswordHashProvider.GetHash(userVM.Password, b64salt);

                var user = _mapper.Map<User>(userVM);
                user.PwdHash = b64hash;
                user.PwdSalt = b64salt;

                _userService.CreateUser(user);

                return RedirectToAction("Login", "User");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
