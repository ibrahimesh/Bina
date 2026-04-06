using Microsoft.AspNetCore.Mvc;
using Bina.BLL.Services.Contracts;
using Bina.BLL.DTOs.Common;
using Bina.BLL.DTOs.Category;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Bina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Retrieves all Parent Categories alongside their corresponding chained Children
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<CategoryDto>>>> GetAllCategoriesAsync()
        {
            var response = await _categoryService.GetAllCategoriesAsync();
            return Ok(response);
        }
    }
}