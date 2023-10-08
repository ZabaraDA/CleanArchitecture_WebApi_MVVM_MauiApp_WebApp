using Backend.Core.Application.Interfaces;
using Backend.Core.Domain.Models;
using Backend.Presentation.DataTransferObjects.Products;
using Backend.Presentation.ServerWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Backend.Presentation.ServerWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        #region Private Members

        private IApplicationDbContext _context;

        #endregion
        #region Conctructor
        public ProductController(IApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Public Context Methods

        [HttpPost]
        public async Task<ApiResponse<Product?>> Add([FromBody] ProductPostDto productPostDto)
        {
            var apiResponse = new ApiResponse<Product?>();
            try
            {
                var success = await _context.Products.AddAsync(new Product
                {
                    Name = productPostDto.Name
                });
                apiResponse.Success = success;
                if (success)
                {
                    var latestProduct = await _context.Products.GetLatest();

                    apiResponse.Result = latestProduct;
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
        public async Task<ApiResponse<List<Product>>> GetAll()
        {
            var apiResponse = new ApiResponse<List<Product>>();

            try
            {
                var productList = await _context.Products.GetAllAsync();
                apiResponse.Success = true;

                apiResponse.Result = productList.ToList();
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
        public async Task<ApiResponse<Product?>> GetById(int id)
        {

            var apiResponse = new ApiResponse<Product?>();

            try
            {
                var Product = await _context.Products.GetByIdAsync(id);
                apiResponse.Success = true;
                apiResponse.Result = Product;
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
        public async Task<ApiResponse<Product?>> Update(Product Product)
        {
            var apiResponse = new ApiResponse<Product?>();

            try
            {
                var success = await _context.Products.UpdateAsync(Product);
                apiResponse.Success = success;
                apiResponse.Result = await _context.Products.GetByIdAsync(Product.Id);
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
                var success = await _context.Products.DeleteAsync(id);
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
