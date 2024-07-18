using WebApp.Domain.Models;
using WebApp.Application.Dto;

namespace WebApp.Domain.Interface
{
    public interface IProductsRepository
    {
        ICollection<Products> GetProducts();
        Products GetProducts(int id);
        Products GetProducts(string name);
        bool ProductExists(int ProductId);
        bool CreateProducts(int categoryId, Products product);
        bool UpdateProducts(int categoryId, Products product);
        bool DeleteProducts(Products product);
        bool Save();
        object GetProductTrimToUpper(ProductDto productCreate);
    }
}
