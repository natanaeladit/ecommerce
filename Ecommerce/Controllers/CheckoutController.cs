using Ecommerce.DomainModels;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class CheckoutController : BaseController
    {
        private readonly IProductRepo _prodRepo;

        public CheckoutController(IProductRepo prodRepo, IShoppingCartRepo cartRepo) : base(cartRepo)
        {
            _prodRepo = prodRepo;
        }

        // GET: Checkout
        public ActionResult Index()
        {
            ViewBag.Cart = _cartRepo.GetAll();
            return View();
        }

        public JsonResult QuantityChange(int type, int pId)
        {
            ShoppingCart cart = _cartRepo.GetByProductId(pId);
            if (cart == null)
            {
                return Json(new { d = "0" });
            }

            Product actualProduct = _prodRepo.Get(pId);
            int quantity;
            // if type 0, decrease quantity
            // if type 1, increase quantity
            switch (type)
            {
                case 0:
                    if (cart.Quantity > 0)
                    {
                        cart.Quantity--;
                        actualProduct.StockQty++;
                    }
                    break;
                case 1:
                    if (actualProduct.StockQty > 0)
                    {
                        cart.Quantity++;
                        actualProduct.StockQty--;
                    }
                    break;
                case -1:
                    actualProduct.StockQty += cart.Quantity;
                    cart.Quantity = 0;
                    break;
                default:
                    return Json(new { d = "0" });
            }

            if (cart.Quantity == 0)
            {
                _cartRepo.Delete(cart.Id);
                quantity = 0;
            }
            else
            {
                quantity = cart.Quantity;
            }
            int totalBasket = _cartRepo.GetAll().Count;

            return Json(new { d = quantity, t = totalBasket });
        }

        [HttpGet]
        public JsonResult UpdateTotal()
        {
            decimal total;
            try
            {
                List<ShoppingCart> carts = _cartRepo.GetAll();
                total = carts.Sum(p => p.UnitPrice * p.Quantity);
            }
            catch (Exception) { total = 0; }

            ViewBag.CartTotalPrice = total;

            return Json(new { d = String.Format("{0:c}", total) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Clear()
        {
            try
            {
                List<ShoppingCart> carts = _cartRepo.GetAll();
                carts.ForEach(a =>
                {
                    Product product = _prodRepo.Get(a.ProductId);
                    product.StockQty += a.Quantity;
                    _prodRepo.Update(product);
                });
                _cartRepo.Clear();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return RedirectToAction("Index", "Home", null);
        }

        public ActionResult Payment()
        {
            ViewBag.Cart = _cartRepo.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Payment(OrderModel model)
        {
            _cartRepo.Clear();
            ViewBag.Cart = _cartRepo.GetAll();
            ViewBag.CartTotalPrice = 0;
            ViewBag.CartUnits = 0;
            return View("PaymentSuccess");
        }
    }
}