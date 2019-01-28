using PersonelMVCUI.Models.EntityFramework;
using PersonelMVCUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonelMVCUI.Controllers
{
    //hem user hem de admin olan kullanıcı login olur
    [Authorize(Roles = "A,U")]
    public class DepartmanController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();
        

        [Authorize]
        public ActionResult Index()
        {
            var per = db.Departman.ToList();
            return View(per);
        }


        [HttpGet]
        public ActionResult Yeni()
        {
            return View("DepartmanForm",new Departman());
        }

        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Departman departman)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmanForm");
            }

            MesajViewModel mesajViewModel = new MesajViewModel();

            if (departman.Id==0)
            {
                db.Departman.Add(departman);
                mesajViewModel.Mesaj = departman.Ad + " başarıyla eklendi.";
            }
            else
            {
                var guncellenecekDepartman = db.Departman.Find(departman.Id);
                if (guncellenecekDepartman==null)
                {
                    return HttpNotFound();
                }
                guncellenecekDepartman.Ad = departman.Ad;
                mesajViewModel.Mesaj = departman.Ad + " başarıyla güncellendi.";
            }
           
            db.SaveChanges();
            mesajViewModel.status = true;
            mesajViewModel.LinkText = "Departman Listesi";
            mesajViewModel.Url = "/Departman";
            return View("_Mesaj",mesajViewModel);
        }

        public ActionResult Guncelle(int id)
        {
            var model = db.Departman.Find(id);
            if (model == null)
                HttpNotFound();
            return View("DepartmanForm",model);

        }

        public ActionResult Sil(int id)
        {
            var silenecekDepartman = db.Departman.Find(id);
            db.Departman.Remove(silenecekDepartman);
            db.SaveChanges();
            return RedirectToAction("Index","Departman");
        }
    }
}