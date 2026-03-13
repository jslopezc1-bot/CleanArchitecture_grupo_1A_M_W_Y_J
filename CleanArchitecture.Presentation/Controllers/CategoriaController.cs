using CleanArchitecture.Application.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaServices _categoriaServices;

        public CategoriaController(ICategoriaServices categoriaServices )
        {
            _categoriaServices = categoriaServices;
        }


        [HttpGet]
        public IActionResult getAllCategories()
        {
            var categories = _categoriaServices.GetAllCategories();
            return Ok(categories);
        }
    }
}
