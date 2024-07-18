using WebApp.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using WebApp.Domain.Models;
using WebApp.Infrastructure.Data;
using WebApp.Application.Dto;


namespace WebApp.Infrastructure.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DataContext _context;
        public ProductsRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateProducts(int categoryId, Products product)
        {
            var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            var productCategory = new ProductsCategory()
            {
                Category = category,
                Products = product,
            };
            _context.Add(productCategory);
            _context.Add(product);
            return Save();
        }

        public bool DeleteProducts(Products product)
        {
            _context.Remove(product);
            return Save();
        }

        public ICollection<Products> GetProducts()
        {
            return _context.Products.OrderBy(p => p.Id).ToList();

        }

        public Products GetProducts(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool ProductExists(int ProductId)
        {
            return _context.Products.Any(p => p.Id == ProductId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProducts(int categoryId, Products product)
        {
            _context.Update(product);
            return Save();
        }

        public Products GetProducts(string name)
        {
            return _context.Products.Where(p => p.Name == name).FirstOrDefault();
        }


        public object GetProductTrimToUpper(ProductDto productCreate)
        {
            throw new NotImplementedException();
        }
    }
}
