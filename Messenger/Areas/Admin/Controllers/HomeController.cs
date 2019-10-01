using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Messenger.Models;

namespace Messenger.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}