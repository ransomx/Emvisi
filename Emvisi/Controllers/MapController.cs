using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emvisi.Controllers
{
    public class MapController : Controller
    {
        // GET: Map
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Map/Test
        public string Test()
        {
            return "Ulala";
        }

        // GET: /Map/GetMarkers
        public JsonResult GetMarkers()
        {
            var data = new { items = new[] {
                new { Id = 1, PlaceName = "Zaghouan", GeoLong = "36.401081", GeoLat = "10.16596", Icon = "dumbbell.png" },
                new { Id = 2, PlaceName = "Hammamet", GeoLong = "36.4", GeoLat = "10.616667" , Icon = "swimming.png" },
                new { Id = 3, PlaceName = "Sousse", GeoLong = "35.8329809", GeoLat = "10.63875" , Icon = "yoga.png" },
                new { Id = 4, PlaceName = "Sfax", GeoLong = "34.745159", GeoLat = "10.7613", Icon = "dumbbell.png" }
            }};
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
 
 
 
 