using Ecommerce.DomainModels;
using Ecommerce.Interfaces;
using System;
using System.Collections.Generic;

namespace Ecommerce.Repositories
{
    public class ShoppingCartRepo : IShoppingCartRepo
    {
        private static List<ShoppingCart> _carts = new List<ShoppingCart>();
        private static int _nextId = 1;

        public ShoppingCartRepo() { }

        public List<ShoppingCart> GetAll()
        {
            return _carts;
        }

        public ShoppingCart Get(int id)
        {
            return _carts.Find(p => p.Id == id);
        }

        public ShoppingCart GetByProductId(int prodId)
        {
            return _carts.Find(p => p.ProductId == prodId);
        }

        public ShoppingCart Add(ShoppingCart cart)
        {
            if (cart == null)
            {
                throw new ArgumentNullException("ShoppingCart");
            }

            cart.Id = _nextId++;
            _carts.Add(cart);
            return cart;
        }

        public bool Update(ShoppingCart cart)
        {
            if (cart == null)
            {
                throw new ArgumentNullException("ShoppingCart");
            }

            int index = _carts.FindIndex(p => p.Id == cart.Id);
            if (index == -1)
            {
                return false;
            }
            _carts.RemoveAt(index);
            _carts.Add(cart);
            return true;
        }

        public bool Delete(int id)
        {
            _carts.RemoveAll(p => p.Id == id);
            return true;
        }

        public bool Clear()
        {
            _carts.Clear();
            return true;
        }
    }
}