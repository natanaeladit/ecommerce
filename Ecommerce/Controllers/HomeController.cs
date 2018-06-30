﻿using Ecommerce.Interfaces;
using Ecommerce.DomainModels;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepo _prodRepo;
        private readonly IShoppingCartRepo _cartRepo;
        
        public HomeController(IProductRepo prodRepo, IShoppingCartRepo cartRepo)
        {
            _prodRepo = prodRepo;
            _cartRepo = cartRepo;
        }

        // GET: Home
        public ActionResult Index()
        {
            var products = _prodRepo.GetAll();
            return View(products);
        }

        public ActionResult AddToCart(int id)
        {
            addToCart(id);
            return RedirectToAction("Index");
        }

        private void addToCart(int pId)
        {
            Product product = _prodRepo.Get(pId);
            if (product != null && product.StockQty > 0)
            {
                // check if product already existed
                ShoppingCart cart = _cartRepo.GetByProductId(pId);
                if (cart != null)
                {
                    cart.Quantity++;
                }
                else
                {
                    cart = new ShoppingCart
                    {
                        ProductName = product.Name,
                        ProductId = product.Id,
                        UnitPrice = product.Price,
                        Quantity = 1
                    };

                    _cartRepo.Add(cart);
                }
                product.StockQty--;
            }
        }
    }
}