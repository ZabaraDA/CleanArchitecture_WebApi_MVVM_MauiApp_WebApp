using Backend.Core.Application.Services;
using Backend.Core.Domain.Models;
using Backend.Presentation.DataTransferObjects.Categories;
using Backend.Presentation.ServerWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Backend.Presentation.ServerWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        #region Private Members

        private IApplicationDbContext _context;

        #endregion

        #region Constructor 
        public CategoryController(IApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Public Context Methods

        [HttpPost]
        public async Task<ApiResponse<Category?>> Add([FromBody] CategoryPostDto categoryPostDto)
        {
            var apiResponse = new ApiResponse<Category?>();
            try
            {
                var success = await _context.Categories.AddAsync(new Category
                {
                    Name = categoryPostDto.Name
                });
                apiResponse.Success = success;
                if (success)
                {
                    var latestCategory = await _context.Categories.GetLatest();

                    apiResponse.Result = latestCategory;
                }
            }
            catch (NpgsqlException ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;

            }

            return apiResponse;
        }
        [HttpGet]
        public async Task<ApiResponse<List<Category>>> GetAll()
        {
            var apiResponse = new ApiResponse<List<Category>>();

            try
            {
                var categoryList = await _context.Categories.GetAllAsync();
                apiResponse.Success = true;

                apiResponse.Result = categoryList.ToList();
            }
            catch (NpgsqlException ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }

            return apiResponse;
        }
        [HttpGet("{id}")]
        public async Task<ApiResponse<Category?>> GetById(int id)
        {

            var apiResponse = new ApiResponse<Category?>();

            try
            {
                var category = await _context.Categories.GetByIdAsync(id);
                apiResponse.Success = true;
                apiResponse.Result = category;
            }
            catch (NpgsqlException ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }

            return apiResponse;
        }
        [HttpPatch]
        public async Task<ApiResponse<Category?>> Update(Category category)
        {
            var apiResponse = new ApiResponse<Category?>();

            try
            {
                var success = await _context.Categories.UpdateAsync(category);
                apiResponse.Success = success;
                apiResponse.Result = await _context.Categories.GetByIdAsync(category.Id);
            }
            catch (NpgsqlException ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }

            return apiResponse;
        }
        [HttpDelete]
        public async Task<ApiResponse<bool>> Delete(int id)
        {
            var apiResponse = new ApiResponse<bool>();

            try
            {
                var success = await _context.Categories.DeleteAsync(id);
                apiResponse.Success = success;
                apiResponse.Result = success;
            }
            catch (NpgsqlException ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }

            return apiResponse;
        }
        #endregion

    }
}
