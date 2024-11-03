using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CastroMotors.Models;

namespace CastroMotors.Controllers
{
    public class CarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cars/Index
        [AdminOnly]
        public ActionResult Index(int? brandId, int? categoryId)
        {
            var cars = db.Cars.Include("Brand").Include("Category").AsQueryable();

            if (brandId.HasValue)
                cars = cars.Where(c => c.BrandId == brandId.Value);

            if (categoryId.HasValue)
                cars = cars.Where(c => c.CategoryId == categoryId.Value);

            ViewBag.Brands = new SelectList(db.Brands, "BrandId", "Name");
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "Name");

            return View(cars.ToList());
        }

        [AdminOnly]
        // GET: Cars/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        [AdminOnly]
        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarId,Model,Year,Color,Price,Description,BrandId,CategoryId")] Car car, HttpPostedFileBase CarImage)
        {
            if (ModelState.IsValid)
            {
                if (CarImage != null && CarImage.ContentLength > 0)
                {
                    var imagePath = Path.Combine(Server.MapPath("~/Images/Cars"), CarImage.FileName);
                    CarImage.SaveAs(imagePath);
                    car.ImagePath = "/Images/Cars/" + CarImage.FileName;
                }

                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name", car.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", car.CategoryId);
            return View(car);
        }

        [AdminOnly]
        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }

            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name", car.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", car.CategoryId);
            return View(car);
        }

        [AdminOnly]
        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "CarId,Model,Year,Color,Price,Description,BrandId,CategoryId,RowVersion")] Car car, HttpPostedFileBase CarImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (CarImage != null && CarImage.ContentLength > 0)
                    {
                        var imagePath = Path.Combine(Server.MapPath("~/Images/Cars"), CarImage.FileName);
                        CarImage.SaveAs(imagePath);
                        car.ImagePath = "/Images/Cars/" + CarImage.FileName;
                    }

                    db.Entry(car).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "O registro foi modificado por outro usuário. Atualize a página e tente novamente.");
                    ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name", car.BrandId);
                    ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", car.CategoryId);
                    return View(car);
                }
            }

            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name", car.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", car.CategoryId);
            return View(car);
        }

        [AdminOnly]
        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Car car = db.Cars.Include("Brand").Include("Category").FirstOrDefault(c => c.CarId == id);

            if (car == null)
            {
                return HttpNotFound();
            }

            return View(car);
        }

        [AdminOnly]
        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            if (!string.IsNullOrEmpty(car.ImagePath))
            {
                var imagePath = Server.MapPath(car.ImagePath);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Car car = db.Cars.Include("Brand").Include("Category").FirstOrDefault(c => c.CarId == id);
            if (car == null)
            {
                return HttpNotFound();
            }

            return View(car);
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
