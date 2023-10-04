using Backend.Core.Application.Services;
using Backend.Core.Domain.Models;
using Backend.Presentation.DataTransferObjects.Products;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Presentation.ServerWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("GetProduct")]
        public async Task<ActionResult> Get(int id)
        {
            Product? product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ProductGetDto productGetDto = new ProductGetDto()
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name
            };
            return Json(productGetDto);
        }


        [HttpGet("GetAllProducts")]
        public async Task<ActionResult> GetAll()
        {
            IEnumerable<Product> productList = await _productRepository.GetAllAsync();
            if (productList.Count() < 1)
            {
                return NotFound("Товары отсутствуют");
            }

            List<ProductGetDto> productDtoList = new();

            foreach (var product in productList)
            {
                productDtoList.Add(new ProductGetDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category.Name
                });
            }

            return Json(productDtoList);
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult<ProductPostDto>> Post([FromBody] ProductPostDto productDto)
        {
            if (string.IsNullOrEmpty(productDto.Name))
            {
                ModelState.AddModelError("Name", "Недопустимо пустое название товара");
            }

            if (productDto.Name?.Length > 300)
            {
                ModelState.AddModelError("Name", "Превышено количество допустимых символов в названии товара");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product? product = new Product()
            {
                Name = productDto.Name,
                CategoryId = productDto.CategoryId,
            };

            await _productRepository.AddAsync(product);

            //product = await _productRepository.GetLatestProduct();
            //if (product == null)
            //{
            //    return BadRequest(new {Message = "Товар не был добавлен"});
            //}

            ProductGetDto productGetDto = new ProductGetDto() 
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name
            };
            return Ok(productGetDto);
        }

        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult> Delete(int id)
        {
            Product? product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                ModelState.AddModelError("Product", "Не существует товара с таким id");
                return BadRequest(ModelState);
            }

            await _productRepository.DeleteAsync(id);

            ProductGetDto productGetDto = new()
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name
            };

            return Ok(new { Message = "Товар успешно удалён", Product = productGetDto});
        }

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<ProductUpdateDto>> Update([FromBody]ProductUpdateDto productUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _productRepository.GetByIdAsync(productUpdateDto.Id) == null)
            {
                return BadRequest(new { Error = "Не существует товара с указанным id" });
            }

            Category? category = await _categoryRepository.GetByIdAsync(productUpdateDto.CategoryId);
            if (category == null)
            {
                return BadRequest(new { Error = "Не существует категории с указанным id" });
            }

            Product product = new()
            {
                Id = productUpdateDto.Id,
                Name = productUpdateDto.Name,
                CategoryId = productUpdateDto.CategoryId,
            };

            await _productRepository.UpdateAsync(product);

            ProductGetDto productGetDto = new()
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                CategoryName = category.Name
            };

            return Ok(new { Message = "Товар успешно обновлён", Product = productGetDto});
        }
    }
}
