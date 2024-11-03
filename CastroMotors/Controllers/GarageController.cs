using CastroMotors.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace CastroMotors.Controllers
{
    [Authorize]
    public class GarageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Garage
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var order = db.Orders
                          .Include("OrderItems.Car")
                          .FirstOrDefault(o => o.UserId == userId && !o.IsFinalized);

            if (order == null)
            {
                order = new Order { UserId = userId, IsFinalized = false, OrderItems = new List<OrderItem>() };
                db.Orders.Add(order);
                db.SaveChanges();
            }

            return View(order);
        }

        // POST: Garage/AddToGarage
        [HttpPost]
        public ActionResult AddToGarage(int carId)
        {
            var userId = User.Identity.GetUserId();
            var order = db.Orders
                          .Include("OrderItems")
                          .FirstOrDefault(o => o.UserId == userId && !o.IsFinalized);

            // Se o pedido não existir, cria um novo com a lista inicializada
            if (order == null)
            {
                order = new Order { UserId = userId, OrderItems = new List<OrderItem>() };
                db.Orders.Add(order);
            }

            // Inicializa OrderItems se estiver null
            if (order.OrderItems == null)
            {
                order.OrderItems = new List<OrderItem>();
            }

            // Verificar se o carro já está na garagem
            if (!order.OrderItems.Any(oi => oi.CarId == carId))
            {
                order.OrderItems.Add(new OrderItem { CarId = carId });
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // POST: Garage/RemoveFromGarage
        [HttpPost]
        public ActionResult RemoveFromGarage(int orderItemId)
        {
            var orderItem = db.OrderItems.Find(orderItemId);
            if (orderItem != null)
            {
                db.OrderItems.Remove(orderItem);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // POST: Garage/Checkout
        [HttpPost]
        public ActionResult Checkout()
        {
            var userId = User.Identity.GetUserId();
            var order = db.Orders
                          .FirstOrDefault(o => o.UserId == userId && !o.IsFinalized);

            if (order != null && order.OrderItems.Any())
            {
                order.IsFinalized = true;
                db.SaveChanges();
                return RedirectToAction("OrderConfirmation");
            }

            return RedirectToAction("Index");
        }

        // GET: Garage/OrderConfirmation
        public ActionResult OrderConfirmation()
        {
            return View();
        }
    }
}
