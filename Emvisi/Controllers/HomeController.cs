using Emvisi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emvisi.Controllers
{
    public class HomeController : Controller
    {
     private BIContext db = new BIContext();

        public ActionResult Index()
        {
            MapViewModels model = new MapViewModels();
            model.Regions = db.Regions.ToList().Select(x => new SelectListItem{
                Value = x.Name,
                Text = x.Name
            });
            model.Activities = db.Activities.ToList().Select(x => new SelectListItem
            {
                Value = x.Title,
                Text = x.Title
            });
            model.Cities = db.Cities.ToList().Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = x.Name
            });
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return PartialView();
        }

        public ActionResult GeneralPage()
        {
            return PartialView();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}