using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using ServiceApp.Models;
using ServiceApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text;

namespace ServiceApp.Controllers
{
    public class AccountController : Controller
    {
        private DatabaseContext db;

        public AccountController(DatabaseContext context) => db = context;

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {

            User user = db.Users
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.Username == model.Username && u.Password == ComputeSha256Hash(model.Password));
            if (user != null)
            {

                user.LastLoginDate = DateTime.Now;
                db.SaveChanges();

                await Authenticate(user);
            }

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        [Authorize]
        [Route("/feed")]
        public IActionResult Feed()
        {
            return View("Profile");
        }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }

        }

        [HttpGet]
        [Route("/user/{username}")]
        public IActionResult User(string username)
        {

            User user = db.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
                return Content("User not found");

            return View(user);

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {

            User user = db.Users.FirstOrDefault(u => u.Email == model.Email || u.Username == model.Username);

            if (user == null)
            {
                User newUser = new User()
                {
                    Name = model.Name,
                    LastName = model.LastName,
                    Email = model.Email,
                    Username = model.Username,
                    Password = ComputeSha256Hash(model.Password),
                    RegisterDate = DateTime.Now,
                    LastLoginDate = DateTime.Now,
                };

                Role userRole = db.Roles.FirstOrDefault(r => r.Name == "user");
                if (userRole != null)
                    newUser.Role = userRole;

                db.Users.Add(newUser);
                await db.SaveChangesAsync();

                await Authenticate(newUser); // аутентификация

            }

            return RedirectToAction("Index", "Home");

        }

        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
