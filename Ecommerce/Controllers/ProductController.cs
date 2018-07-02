using Ecommerce.DomainModels;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using System.Net;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepo _prodRepo;
        public ProductController(IProductRepo prodRepo)
        {
            _prodRepo = prodRepo;
        }

        // GET: Product
        public ActionResult Index()
        {
            if (Session["Login"] == null)
                return RedirectToAction("login", "Admin");

            var products = _prodRepo.GetAll();
            return View(products);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            if (Session["Login"] == null)
                return RedirectToAction("login", "Admin");

            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel prodModel)
        {
            if (Session["Login"] == null)
                return RedirectToAction("login", "Admin");

            try
            {
                if (ModelState.IsValid)
                {
                    _prodRepo.Add(new Product()
                    {
                        Name = prodModel.Name,
                        Description = prodModel.Description,
                        Price = prodModel.Price,
                        StockQty = prodModel.StockQty
                    });
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                // TODO log error
                ModelState.AddModelError("", "Something went wrong, please try again");
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["Login"] == null)
                return RedirectToAction("login", "Admin");

            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _prodRepo.Get(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ProductModel model = new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQty = product.StockQty
            };
            return View(model);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProductModel model)
        {
            if (Session["Login"] == null)
                return RedirectToAction("login", "Admin");

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (id < 0 || id != model.Id)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var product = _prodRepo.Get(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.StockQty = model.StockQty;
                _prodRepo.Update(product);

                return RedirectToAction("Index");
            }
            catch
            {
                // TODO log error
                ModelState.AddModelError("", "Something went wrong, please try again");
                return RedirectToAction("Index");
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["Login"] == null)
                return RedirectToAction("login", "Admin");
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _prodRepo.Get(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ProductModel model = new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQty = product.StockQty
            };
            return View(model);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ProductModel model)
        {
            if (Session["Login"] == null)
                return RedirectToAction("login", "Admin");

            try
            {
                if (id < 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var product = _prodRepo.Get(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                _prodRepo.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                // TODO log error
                ModelState.AddModelError("", "Something went wrong, please try again");
                return View(model);
            }
        }
    }
}
