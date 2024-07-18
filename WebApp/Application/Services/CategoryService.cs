using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WebApp.Application.Dto;
using WebApp.Domain.Interface;
using WebApp.Domain.Models;

namespace WebApp.Application.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public List<CategoryDto> GetCategories()
        {
            return _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());
        }

        public CategoryDto GetCategory(int categoryId)
        {
            return _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(categoryId));
        }

        public List<ProductDto> GetProductsByCategoryId(int categoryId)
        {
            return _mapper.Map<List<ProductDto>>(_categoryRepository.GetProductByCategory(categoryId));
        }

        public bool CreateCategory(CategoryDto categoryCreate)
        {
            var category = _categoryRepository.GetCategories()
                .FirstOrDefault(c => c.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper());

            if (category != null)
                return false;

            var categoryMap = _mapper.Map<Category>(categoryCreate);
            return _categoryRepository.CreateCategory(categoryMap);
        }

        public bool UpdateCategory(int categoryId, CategoryDto updatedCategory)
        {
            if (categoryId != updatedCategory.Id)
                return false;

            if (!_categoryRepository.CategoryExists(categoryId))
                return false;

            var categoryMap = _mapper.Map<Category>(updatedCategory);
            return _categoryRepository.UpdateCategory(categoryMap);
        }

        public bool DeleteCategory(int categoryId)
        {
            var categoryToDelete = _categoryRepository.GetCategory(categoryId);
            return _categoryRepository.DeleteCategory(categoryToDelete);
        }

        internal bool CategoryExists(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
