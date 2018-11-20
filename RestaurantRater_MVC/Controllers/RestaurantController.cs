using RestaurantRater_MVC.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace RestaurantRater_MVC.Controllers {
    public class RestaurantController : Controller {
        private RestaurantDBContext db = new RestaurantDBContext();

        // GET: Restaurant
        public ActionResult Index() {
            return View(db.Restaurants.ToList());
        }

        
        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RestaurantID,Name,Address,Rating")] Restaurant restaurant) {
            if (ModelState.IsValid) {
                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restaurant);
        }

        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Restaurant restaurant = db.Restaurants.Find(id);

            if (restaurant == null) {
                return HttpNotFound();
            }

            return View(restaurant);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Restaurant restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Restaurant restaurant = db.Restaurants.Find(id);

            if (restaurant == null) {
                return HttpNotFound();
            }

            return View(restaurant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include ="RestaurantID,Name,Address,Rating")] Restaurant restaurant) {
            if (ModelState.IsValid) {
                db.Entry(restaurant).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restaurant);
        }
    }
}