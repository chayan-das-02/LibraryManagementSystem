using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    public class UserController : Controller
    {
        static string KeyUser_String = "";
        IUsersRepository _usersRepository;
        public UserController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UsersModel user)
        {
            bool RegisterStatus = _usersRepository.Register(user);
            if (RegisterStatus)
            {
                TempData["successMessage"] = "You are eligible to Login, Please fill own credential's then Login";
                return RedirectToAction("Login", "User");
            }
            else
            {
                TempData["errorMessage"] = "Empty form or User ID is already taken";
                return View();
            }

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserLoginModel user)
        {
            UsersModel userFound = _usersRepository.Login(user);
            if (userFound != null)
            {
                if (user.user_name == "Admin" && userFound.role_id == 1 && user.password == userFound.password)
                {

                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.user_name),
                    new Claim(ClaimTypes.Role,"Admin")},
                        CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    HttpContext.Session.SetString("user_name", user.user_name);
                    KeyUser_String = user.user_name;
                    return RedirectToAction("Index", "Book");
                }
                else if (user.user_name == userFound.user_name && user.password == userFound.password)
                {
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.user_name),
                    new Claim(ClaimTypes.Role,"User")},
                        CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    HttpContext.Session.SetString("user_name", user.user_name);
                    return RedirectToAction("Index", "Home");
                }
                TempData["errorPassword"] = "Wrong Password";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["errorUserId"] = "User Not found";
                return RedirectToAction("Login");
            }

        }

        public IActionResult Logout()
        {

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var storedCookies = Request.Cookies.Keys;
            foreach (var cookies in storedCookies)
            {

                Response.Cookies.Delete(cookies);
            }
            return RedirectToAction("Login", "User");
        }

        public async Task<IActionResult> GetAllUserAsync()
        {
            var users = await _usersRepository.GetAllUser();
            return View(users);
        }

        public IActionResult actionResult(UsersModel usersModel)
        {
            ViewBag.text = _usersRepository.Show(usersModel);
            return View();
        }
    }
}
