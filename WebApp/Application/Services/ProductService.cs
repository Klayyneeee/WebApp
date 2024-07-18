using AutoMapper;
using System.Collections.Generic;
using WebApp.Application.Dto;
using WebApp.Domain.Interface;
using WebApp.Domain.Models;

namespace WebApp.Application.Services
{
    public class ProductService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        public List<ProductDto> GetProducts()
        {
            return _mapper.Map<List<ProductDto>>(_productsRepository.GetProducts());
        }

        public ProductDto GetProduct(int productId)
        {
            return _mapper.Map<ProductDto>(_productsRepository.GetProducts(productId));
        }

        public bool UpdateProduct(int productId, int categoryId, ProductDto updatedProduct)
        {
            if (productId != updatedProduct.Id || !_productsRepository.ProductExists(productId))
                return false;

            var productMap = _mapper.Map<Products>(updatedProduct);
            return _productsRepository.UpdateProducts(categoryId, productMap);
        }

        public bool CreateProduct(int categoryId, ProductDto productCreate)
        {
            var products = _productsRepository.GetProductTrimToUpper(productCreate);

            if (products != null)
                return false;

            var productMap = _mapper.Map<Products>(productCreate);
            return _productsRepository.CreateProducts(categoryId, productMap);
        }

        public bool DeleteProduct(int productId)
        {
            var productToDelete = _productsRepository.GetProducts(productId);
            return _productsRepository.DeleteProducts(productToDelete);
        }

        public bool ProductExists(int productId)
        {
            return _productsRepository.ProductExists(productId);
        }
    }
}
