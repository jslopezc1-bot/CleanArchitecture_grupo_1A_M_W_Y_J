using CleanArchitecture.Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Interface.Services
{
    public interface ICategoriaServices
    {
        CategoryDto GetCategoryById(int id);
        CategoryDto GetCategoryByName(string name);
        IEnumerable<CategoryDto> GetAllCategories();
        void CreateCategory(CategoryDto category);
        void UpdateCategory(int id, CategoryDto category);
        void DeleteCategory(int id);
    }
}
