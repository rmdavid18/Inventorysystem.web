using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RenielDavidInventorySystem.Infrastructure.Domain;
using RenielDavidInventorySystem.Infrastructure.Domain.Models;
using RenielDavidInventorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RenielDavidInventorySystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly DefaultDbContext _context;
        protected readonly IConfiguration _config;
        private string emailUserName;
        private string emailPassword;
        public AccountController(DefaultDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            var emailConfig = this._config.GetSection("Email");
            emailUserName = emailConfig["Username"].ToString();
            emailPassword = emailConfig["Password"].ToString();
        }
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Login");
                return View(model);
            }

            User user = _context.Users.FirstOrDefault(u => u.Email.ToLower() == model.EmailAddress.ToLower());

            if (user == null)
            {
                ModelState.AddModelError("Error", "Invalid Login");
                return View(model);
            }
            else
            {
                if (BCrypt.BCryptHelper.CheckPassword(model.Password, user.Password))
                {
                    await SignIn(user);
                    return RedirectToAction("/");
                }
                ModelState.AddModelError("Error", "Invalid Login.");
                return View(model);
            }
        }

        [HttpGet("Account/CreateAccount")]
        public IActionResult CreateAccount()
        {
            return View();
        }
        [HttpPost, Route("Account/CreateAccount")]
        public IActionResult Create(UserViewModel model)
        {
            var salt = BCrypt.BCryptHelper.GenerateSalt();
            var password = RandomString(6);
            var hashedPassword = BCrypt.BCryptHelper.HashPassword(password, salt);

            User user = new User()
            {
                Email = model.Email,
                Password = hashedPassword,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                BirthDate = model.BirthDate,
                PhoneNumber = model.PhoneNumber,
                Role = Infrastructure.Domain.Models.Enums.Role.Admin,
                Sex = model.Sex,
                UserID = Guid.NewGuid()
            };
            var fullname = model.FirstName + model.LastName;
            _context.Users.Add(user);
            _context.SaveChanges();

            this.SendNow("Hello " + fullname + " Please use this one time password to login:" + password, model.Email, "Peninsula Account Registration", "Welcome to Peninsula!");

            return Redirect("~/");
        }


        private Random random = new Random();
        private string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void SendNow(string message, string messageTo, string messageName, string emailSubject)
        {
            var fromAddress = new MailAddress(emailUserName, "CSM Bataan Apps");
            string body = message;


            ///https://support.google.com/accounts/answer/6010255?hl=en
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, emailPassword),
                Timeout = 20000
            };

            var toAddress = new MailAddress(messageTo, messageName);

            smtp.Send(new MailMessage(fromAddress, toAddress)
            {
                Subject = emailSubject,
                Body = body,
                IsBodyHtml = true
            });

        }
        public async Task SignIn(User user)
        {
            var identity = new System.Security.Claims.ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.FirstName));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));

            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true,
                IssuedUtc = DateTimeOffset.UtcNow
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

            //WebUser.SetUser(user);
        }
    }
}
