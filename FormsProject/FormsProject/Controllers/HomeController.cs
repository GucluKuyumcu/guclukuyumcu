using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FormsProject.Dto;
using System.Net.Http;
using FormsProject.Models;

namespace FormProject.Controllers
{
    public class HomeController : Controller
    {      
        public ActionResult Index()
        {
            if (Session["Kullanici"]==null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [HttpPost]
        public JsonResult FormInsertUpdate(FormDetailsModel model)
        {
            FormsEntities db = new FormsEntities();
            jModel sonuc = new jModel();
            if (model.FormID == 0)
            {
                FormDetails yeniKayit = new FormDetails();
                yeniKayit.name = model.Name;
                yeniKayit.description = model.Description;
                yeniKayit.createdAt = DateTime.Now;
                yeniKayit.createdBy = Session["Kullanici"].ToString();
                db.FormDetails.Add(yeniKayit);
                db.SaveChanges();
                sonuc.islem = true;
                sonuc.mesaj = "Kayıt Eklendi";
            }
            else
            {
                FormDetails kayit = db.FormDetails.Where(m => m.formId == model.FormID).SingleOrDefault();
                kayit.name = model.Name;
                kayit.description = model.Description;
                kayit.createdAt = DateTime.Now;
                kayit.createdBy = Session["Kullanici"].ToString();
                db.SaveChanges();
                sonuc.islem = true;
                sonuc.mesaj = "Kayıt Güncellendi";
            }
            return Json(sonuc, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FormsList(int? id)
        {
            FormsEntities db = new FormsEntities();
            FormDetails kayit = db.FormDetails.Where(m => m.formId == id).SingleOrDefault();
            FormDetailsModel model = new FormDetailsModel();
            if (kayit != null)
            {
                model.FormID = kayit.formId;
                model.Name = kayit.name;
                model.Description = kayit.description;
            }         
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FormDelete(int? id)
        {
            FormsEntities db = new FormsEntities();
            jModel sonuc = new jModel();
            FormDetails kayit = db.FormDetails.Where(m => m.formId == id).SingleOrDefault();
            if (kayit != null)
            {
                db.FormDetails.Remove(kayit);
                db.SaveChanges();
                sonuc.islem = true;
                sonuc.mesaj = "Kayıt Silindi";
            }
            else
            {
                sonuc.islem = false;
                sonuc.mesaj = "Hata";
            }
            return Json(sonuc, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _FormDetailList()
        {
            FormsEntities db = new FormsEntities();
            List<FormDetails> kayitlar = db.FormDetails.ToList();
            return PartialView(kayitlar);
        }

        [HttpPost]
        public JsonResult FormSearch(string Searched)
        {
            FormsEntities db = new FormsEntities();
            List<FormDetails> searchedRows = new List<FormDetails>();

            if (!string.IsNullOrEmpty(Searched))
            {
                searchedRows = db.FormDetails.Where(s=> s.name.Contains(Searched)).ToList();             
            }

            return Json(searchedRows, JsonRequestBehavior.AllowGet);
        }

    }
}




