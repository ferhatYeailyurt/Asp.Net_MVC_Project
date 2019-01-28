 using PersonelMVCUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PersonelMVCUI.ViewModels;

namespace PersonelMVCUI.Controllers
{
    //hem user hem de admin olan kullanıcı login olur
    [Authorize(Roles ="A,U")]
    public class PersonelController : Controller
    {

        PersonelDbEntities db = new PersonelDbEntities();


        
        // GET: Personel
        public ActionResult Index()
        {
            var personel=db.Personel.Include(x=>x.Departman).ToList();
            return View(personel);
        }


        public ActionResult Yeni()
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                personel=new Personel()
            };
            return View("PersonelForm",model);
        }


        
        public ActionResult Kaydet(Personel personel)
        {
            if (!ModelState.IsValid)
            {
                var model = new PersonelFormViewModel()
                {
                    Departmanlar = db.Departman.ToList(),
                    personel = personel
                };
                return View("PersonelForm", model);
            }
            if (personel.Id==0)//ekleme işlemi
            {
                db.Personel.Add(personel);
            }
            else//güncelleme işlemi
            {
                db.Entry(personel).State = System.Data.Entity.EntityState.Modified;

            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Guncelle(int id)
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                personel = db.Personel.Find(id)
            };
            return View("PersonelForm",model);
        }



        public ActionResult Sil(int id)
        {
            var silenecek = db.Personel.Find(id);
            if (silenecek==null)
            {
                return HttpNotFound();
            }
            db.Personel.Remove(silenecek);
            db.SaveChanges();

            return RedirectToAction("Index");

        }


        public ActionResult PersonelleriListele(int id)
        {
            var model = db.Personel.Where(x => x.DepartmanId == id).ToList();
            return PartialView(model);
        }






    }


}