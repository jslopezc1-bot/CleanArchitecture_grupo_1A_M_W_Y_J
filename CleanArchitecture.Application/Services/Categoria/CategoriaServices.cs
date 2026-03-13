using CleanArchitecture.Application.Dto;
using CleanArchitecture.Application.Interface;
using CleanArchitecture.Application.Interface.Services;
using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Services.Categoria
{
    public class CategoriaServices : ICategoriaServices
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaServices(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public void CreateCategory(CategoryDto category)
        {
            Domain.Entities.Categoria categoria = new Domain.Entities.Categoria
            {
                Name = category.Name,
                Activo = true,
                Eliminado = false,
            };

            _categoriaRepository.Add(categoria);
        }

        public void DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            IEnumerable<Domain.Entities.Categoria> categorias = _categoriaRepository.GetAll();

            List<CategoryDto> categoryDtos = new List<CategoryDto>();
            foreach (var categoria in categorias)
            {
                categoryDtos.Add(new CategoryDto
                {
                    Id = categoria.Id,
                    Name = categoria.Name
                });
            }
            return categoryDtos;
        }

        public CategoryDto GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public CategoryDto GetCategoryByName(string name)
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(int id, CategoryDto category)
        {
            throw new NotImplementedException();
        }
    }
}
