using Ecommerce.DomainModels;
using System.Collections.Generic;

namespace Ecommerce.Interfaces
{
    public interface IShoppingCartRepo
    {
        List<ShoppingCart> GetAll();
        ShoppingCart Get(int id);
        ShoppingCart GetByProductId(int prodId);
        ShoppingCart Add(ShoppingCart cart);
        bool Update(ShoppingCart cart);
        bool Delete(int id);
    }
}