using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Emvisi.Models;

namespace Emvisi.Controllers
{
    public class GymsController : Controller
    {
        private BIContext db = new BIContext();

        // GET: Gyms
        public ActionResult Index()
        {
            return View(db.Gyms.ToList());
        }

        // GET: Gyms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gym gym = db.Gyms.Find(id);
            if (gym == null)
            {
                return HttpNotFound();
            }
            return View(gym);
        }

        // GET: Gyms/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Gyms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterGymModel model) {
             model.Regions = db.Regions.ToList().Select(x => new SelectListItem
            {
                Value = x.RegionId.ToString(),
                Text = x.Name
            });
            model.Cities = db.Cities.ToList().Select(x => new SelectListItem
            {
                Value = x.CityId.ToString(),
                Text = x.Name
            });
            return ContinueGymRegisration(model);
        }

        // GET: Gyms/ContinueGymRegisration
        public ActionResult ContinueGymRegisration(RegisterGymModel model)
        {
             return PartialView("ContinueGymRegistration", model);
        }

        // POST: Gyms/CompleteGymRegistration
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompleteGymRegistration(RegisterGymModel model)
        {
            Gym gym = new Gym {
                Title = model.Title,
                BrandName = model.Brand,
                Description = model.Description,
                LogoFilePath = model.Logo,
                Position = new Position { GeoLat = model.GeoLat, GeoLong = model.GeoLoc },
                ContactInfo = new ContactInfo { Email = model.Email, Phone = model.Phone,
                    Address = new Address {
                        City = db.Cities.FirstOrDefault(c => c.CityId==model.SelectCity),
                        Street = model.Street,
                        Zip = model.Zip
                    }
                }
            };
            ViewBag.GymTitle = model.Title;
            ViewBag.GymEmail = model.Email;
            return PartialView("GymRegComplete");
        }

        // GET: Gyms/GymRegComplete
        public ActionResult GymRegComplete()
        {
            return PartialView();
        }

        // GET: Gyms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gym gym = db.Gyms.Find(id);
            if (gym == null)
            {
                return HttpNotFound();
            }
            return View(gym);
        }

        // POST: Gyms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GymId,Title,Description,BrandName,LogoFilePath")] Gym gym)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gym).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gym);
        }

        // GET: Gyms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gym gym = db.Gyms.Find(id);
            if (gym == null)
            {
                return HttpNotFound();
            }
            return View(gym);
        }

        // POST: Gyms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gym gym = db.Gyms.Find(id);
            db.Gyms.Remove(gym);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
