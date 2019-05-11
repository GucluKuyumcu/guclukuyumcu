using FormsProject.Dto;
using FormsProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FormProject.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (String.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                FormsAuthentication.SignOut();
                return View();
            }
            return Redirect("/Index/Home");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserModel model, string returnUrl)
        {
            FormsEntities db = new FormsEntities();    
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.userName == model.UserName && u.password == model.Password);
                if (user != null)
                {
                    Session["Kullanici"] = model.UserName;
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı!");
                }
            }
            return View(model);
        }
    }
}