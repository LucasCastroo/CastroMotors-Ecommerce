using System.Linq;
using System.Web.Mvc;
using CastroMotors.Models;

namespace CastroMotors.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? brandId, int? categoryId)
        {
            var cars = db.Cars.Include("Brand").Include("Category").AsQueryable();

            // Aplica o filtro de marca, se selecionado
            if (brandId.HasValue)
                cars = cars.Where(c => c.BrandId == brandId.Value);

            // Aplica o filtro de categoria, se selecionado
            if (categoryId.HasValue)
                cars = cars.Where(c => c.CategoryId == categoryId.Value);

            // Carrega as listas de marcas e categorias para o dropdown
            ViewBag.Brands = new SelectList(db.Brands, "BrandId", "Name");
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "Name");

            return View(cars.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Title = "About";
            ViewBag.Message = "Página Sobre a Castro Motors";
            return View();
        }

        public ActionResult AcessoNegado()
        {
            return View();
        }

    }
}
