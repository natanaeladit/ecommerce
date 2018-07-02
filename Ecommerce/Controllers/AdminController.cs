using Ecommerce.Models;
using Ecommerce.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Login"] == null)
                return RedirectToAction("Login");
            else
            {
                return RedirectToAction("Index", "Product");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel model)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings[Constants.ADMIN_DEMO_IS_ACTIVE]))
            {
                if (model.Email == ConfigurationManager.AppSettings[Constants.ADMIN_DEMO_EMAIL] && 
                    model.Password == ConfigurationManager.AppSettings[Constants.ADMIN_DEMO_PASSWORD])
                {
                    Session["Login"] = model.Email;
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View();
                }
                
            }
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}