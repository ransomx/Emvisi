using Emvisi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emvisi.Controllers
{
    public class MapController : Controller
    {
        private BIContext db = new BIContext();

        // GET: Map
        public ActionResult Index()
        {
            return View();
        }


        // GET: Cities
        public JsonResult Cities(string region)
        {
           Region selectedRegion = db.Regions.FirstOrDefault(m => m.Name == region);
           var result = db.Cities.Where(m => m.Region.RegionId == selectedRegion.RegionId).Select(a => new { Id = a.CityId, Value = a.Name });
           return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Cities
        public JsonResult Cities2(string region)
        {
            Region selectedRegion = db.Regions.FirstOrDefault(m => m.Name == region);
            var result = db.Cities.Where(m => m.Region.RegionId == selectedRegion.RegionId).Select(a => new { Id = a.CityId, Value = a.Name });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: /Map/Test
        public string Test()
        {
            return "Ulala";
        }

        // GET: /Map/GetMarkers
        public JsonResult GetMarkers(string selectRegion, string selectCity, string selectActivity, string searchString)
        {/*
            var data = from gym in db.Gyms
                       where gym.Title.Contains(search)
                       select gym;*/

            var data = (from gym in db.Gyms
                       where (searchString == null || gym.Title.Contains(searchString)) 
                       && (((selectRegion == null || selectRegion=="") && (selectCity == null || selectCity == "")) 
                       || (selectRegion == gym.ContactInfo.Address.City.Region.Name && (selectCity == null || selectCity == ""))
                       || (selectCity == gym.ContactInfo.Address.City.Name))
                       && ((selectActivity == null || selectActivity == "") || gym.HasActivities.Any(t=>t.Title==selectActivity))
                       select new
                       {
                           Id = gym.GymId,
                           Title = gym.Title,
                           Desc = gym.Description,
                           GeoLat = gym.Position.GeoLat,
                           GeoLong = gym.Position.GeoLong,
                           Logo = gym.LogoFilePath,
                           City = gym.ContactInfo.Address.City.Name,
                           Region = gym.ContactInfo.Address.City.Region,
                           Street = gym.ContactInfo.Address.Street,
                           Zip = gym.ContactInfo.Address.Zip,
                       }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
 
 
 
 