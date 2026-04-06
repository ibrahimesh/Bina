using Bina.BLL.DTOs.Common;
using Bina.BLL.DTOs.Category;
using Bina.BLL.Services.Contracts;
using Bina.DAL.Repositories.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Bina.BLL.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<CategoryDto>>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                var dtos = _mapper.Map<IEnumerable<CategoryDto>>(categories.Where(c => c.ParentId == null));
                return ApiResponse<IEnumerable<CategoryDto>>.Ok(dtos);
            }
            catch (System.Exception ex)
            {
                return ApiResponse<IEnumerable<CategoryDto>>.Fail("Kateqoriyalar yükl?n?rk?n x?ta", ex.Message);
            }
        }
    }
}