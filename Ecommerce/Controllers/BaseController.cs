using Ecommerce.DomainModels;
using Ecommerce.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IShoppingCartRepo _cartRepo;

        public BaseController(IShoppingCartRepo cartRepo)
        {
            _cartRepo = cartRepo;
            ViewBag.CartTotalPrice = CartTotalPrice;
            ViewBag.Cart = Cart;
            ViewBag.CartUnits = Cart.Count;
        }

        private List<ShoppingCart> Cart
        {
            get
            {
                return _cartRepo.GetAll();
            }
        }

        private decimal CartTotalPrice
        {
            get
            {
                return Cart.Sum(c => c.Quantity * c.UnitPrice);
            }
        }
    }
}