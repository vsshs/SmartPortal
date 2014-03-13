using System.Web.Mvc;
using SmartPortal.Web.Models;
using WebMatrix.WebData;

namespace SmartPortal.Web.Controllers
{
    public class LogonController : Controller
    {
        [AllowAnonymous]
        public ActionResult Logon()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Logon(LogonModel model)
        {
            /*
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
             */

            return null;
        }
    }
}
