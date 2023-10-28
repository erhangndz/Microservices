using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Catalog.Services.Interfaces
{
    interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();

        Task<Response<CategoryDto>> GetByIdAsync(string id);

        Task<Response<CategoryDto>> CreateAsync(CreateCategoryDto category);
    }
}
