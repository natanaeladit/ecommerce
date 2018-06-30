using Ecommerce.DomainModels;
using System.Collections.Generic;

namespace Ecommerce.Interfaces
{
    public interface IProductRepo
    {
        List<Product> GetAll();
        Product Get(int productId);
        Product Add(Product prod);
        bool Update(Product prod);
        bool Delete(int id);
    }
}