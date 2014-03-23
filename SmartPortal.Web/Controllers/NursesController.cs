using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartPortal.Model;
using SmartPortal.Web.Infrastructure;
using SmartPortal.Web.Models;
using SmartPortal.Web.Models_API;
using SmartPortal.Web.ViewModels;

namespace SmartPortal.Web.Controllers
{
    public class NursesController : Controller
    {
        //
        // GET: /Nurses/
        public ActionResult Index()
        {
            var nurses = Portal.Instance().GetNurses();
            return View(nurses);
        }


        public ActionResult CreateNurse()
        {
            var viewModel = new NurseViewModel();
            return PartialView("_CreateNurse", viewModel);
        }

        [HttpPost]
        public ActionResult CreateNurse(NurseViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var nurse = Portal.Instance().AddNurse(new Nurse
                {
                    Name = model.Name,
                    Pin = model.Pin
                });
                return PartialView("_Nurse", nurse); 
            }
            return null;
        }

	}
}