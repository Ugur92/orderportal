using DataAccess;
using DataAccess.Models;
using LedSiparisModulu.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LedSiparisModulu.Controllers
{
    public class AccountController : BaseController
    {
        private readonly LAF_BUPILICContext db;
        private readonly LogoDbContext _logoDbContext;
        private readonly ILogger _logger;

        public AccountController(LAF_BUPILICContext context, LogoDbContext logoDbContext,
            ILogger<AccountController> logger)
        {
            db = context;
            _logoDbContext = logoDbContext;
            _logger = logger;
        }

        [HttpGet("oturum-ac", Name = "Login")]
        public IActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToRoute("Anasayfa");
            }

            return View();
        }

        [HttpPost("oturum-ac", Name = "LoginPost")]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToRoute("Anasayfa");
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            Chtanim kul = db.Chtanim.AsNoTracking().FirstOrDefault(u => u.B2bkullaniciadi == model.KullaniciAdi && u.B2bparola == model.Sifre);
            if (kul == null)
            {
                AddToastMessage("Bupiliç Entegre A.Ş.", "Kullanıcı Adı veya Şifre Hatalı", Infrastructures.ToastType.warning);
                return View(model);
            }

            //Login Log
            db.WebappLoginLog.Add(new WebappLoginLog()
            {
                Chid = kul.Id,
                Tarih = DateTime.Now
            });
            db.SaveChanges();

            _logger.LogTrace($"{kul.Id}'li kullanici oturum acti");
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, kul.B2bkullaniciadi),
                new Claim("Id", kul.Id.ToString()),
                new Claim("Chkod", kul.Kod),
                new Claim("Chunvan", kul.Unvan)
            };

            TempData.Clear();
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties());
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("cikis-yap", Name = "Cikis")]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("User {Name} logged out at {Time}.", User.Identity.Name, DateTime.UtcNow);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToRoute("Login");
        }
    }
}