using Ecommerce.Interfaces;
using Ecommerce.DomainModels;
using System;
using System.Collections.Generic;

namespace Ecommerce.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private static List<Product> _products = new List<Product>();
        private static int _nextId = 1;
        private static bool _isDataInitialized = false;

        public ProductRepo()
        {
            if(!_isDataInitialized)
            {
                Add(new Product() { Name = "Product A", Description = "This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.", Price = 10, StockQty = 3 });
                Add(new Product() { Name = "Product B", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", Price = 20, StockQty = 4 });
                Add(new Product() { Name = "Product C", Description = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.", Price = 30, StockQty = 5 });
                Add(new Product() { Name = "Product D", Description = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.", Price = 40, StockQty = 3 });
                Add(new Product() { Name = "Product E", Description = "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Price = 50, StockQty = 2 });
                _isDataInitialized = true;
            }
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public Product Get(int productId)
        {
            return _products.Find(p => p.Id == productId);
        }

        public Product Add(Product prod)
        {
            if (prod == null)
            {
                throw new ArgumentNullException("Product");
            }
            
            prod.Id = _nextId++;
            _products.Add(prod);
            return prod;
        }

        public bool Update(Product prod)
        {
            if (prod == null)
            {
                throw new ArgumentNullException("Product");
            }
            
            int index = _products.FindIndex(p => p.Id == prod.Id);
            if (index == -1)
            {
                return false;
            }
            _products.RemoveAt(index);
            _products.Add(prod);
            return true;
        }

        public bool Delete(int id)
        {
            _products.RemoveAll(p => p.Id == id);
            return true;
        }
    }
}