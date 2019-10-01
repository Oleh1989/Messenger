using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web;
using System.Web.Mvc;
using Messenger.Models;
using Messenger.Encryption;

namespace Messenger.Controllers
{
    public class AccountController : Controller
    {
        UserContext db = new UserContext();        
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    string encryptedPassword = Protector.Encrypt(model.Password);
                    // Searching user in db
                    User user = db.Users.FirstOrDefault(u => u.Login == model.Name && u.Password == encryptedPassword);

                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);

                        if (user.RoleId == 1)
                        {
                            return RedirectToAction("Index", "Home", new { Area = "Admin" });
                        }

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "There is no user with such login and password");
                    }
                }                
            }
            return View(model);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {                    
                    User user = db.Users.FirstOrDefault(u => u.Login == model.Name && u.Password == model.Password);
                    if (user == null)
                    {
                        string encryptedPassword = Protector.Encrypt(model.Password);
                        db.Users.Add(new User { Login = model.Name, Password = encryptedPassword, CreationDate = DateTime.Now, RoleId = 2 });
                        db.SaveChanges();
                        user = db.Users.Where(u => u.Login == model.Name && u.Password == encryptedPassword).FirstOrDefault();
                        if (user != null)
                        {
                            FormsAuthentication.SetAuthCookie(model.Name, true);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                        ModelState.AddModelError("", "User with such login and password already exists");
                }                
            }
            return View(model);
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}