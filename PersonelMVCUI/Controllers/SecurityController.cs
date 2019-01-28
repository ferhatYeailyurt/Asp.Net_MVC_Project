using PersonelMVCUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PersonelMVCUI.Controllers
{
    
    public class SecurityController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();

        // GET: Security
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Kullanici kullanici)
        {

            var kullaniciInDbn = db.Kullanici.FirstOrDefault(x => x.Ad == kullanici.Ad && x.Sifre == kullanici.Sifre);
                if (kullaniciInDbn!=null)
                {
                FormsAuthentication.SetAuthCookie(kullaniciInDbn.Ad, false);
                return RedirectToAction("Index","Departman");
                }
            else
            {
                ViewBag.Mesaj = "Geçersiz kullanıcı adı veya şifre...";
                return View();
            }
           

        }

        public ActionResult Loguot()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}