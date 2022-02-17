using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TektonProductsAPI.Models;
using TektonProductsAPI.Models.Dtos;
using TektonProductsAPI.Repository.IRepository;

namespace TektonProductsAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _prdRepo;
        private readonly IMapper _mapper;
        public ProductsController(IProductsRepository prdRepo, IMapper mapper)
        {
            _prdRepo = prdRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var productsList = _prdRepo.GetProducts();

            var productListDto = new List<ProductsDtos>();

            foreach (var product in productsList)
            {
                productListDto.Add(_mapper.Map<ProductsDtos>(product));
            }

            return Ok(productListDto);
        }

        [HttpGet("{productGuid:Guid}", Name = "GetProduct")]
        public IActionResult GetProduct(Guid productGuid)
        {
            var singleProduct = _prdRepo.GetProduct(productGuid);
            if (singleProduct == null)
            {
                return NotFound();
            }

            var singleProductDto = _mapper.Map<ProductsDtos>(singleProduct);
            return Ok(singleProductDto);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductsDtos productsDtos)
        {
            if (productsDtos == null)
            {
                return BadRequest(ModelState);
            }
            if (_prdRepo.ProductExists(productsDtos.Name))
            {
                ModelState.AddModelError("", "Product already exists.");
                return StatusCode(404, ModelState);
            }
            var product = _mapper.Map<Product>(productsDtos);

            if (!_prdRepo.CreateProduct(product))
            {
                ModelState.AddModelError("", $"An error occured when saving product{product.Name}");
                return StatusCode(500, ModelState);
            }
            return Ok(product);
        }

        [HttpPatch("{productGuid:Guid}", Name = "UpdateProduct")]
        public IActionResult UpdateProduct(Guid productGuid, [FromBody]ProductsDtos productsDtos) 
        {
            if (productsDtos == null || productGuid != productsDtos.Id) 
            {
                return BadRequest(ModelState);
            }
            var product = _mapper.Map<Product>(productsDtos);
            if (!_prdRepo.UpdateProduct(product)) 
            {
                ModelState.AddModelError("", $"Error ocurred while updating record {product.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{productGuid:Guid}", Name = "DeleteProduct")]
        public IActionResult DeleteProduct(Guid productGuid)
        {
            if (!_prdRepo.ProductExists(productGuid))
            {
                return NotFound();
            }

            var product = _prdRepo.GetProduct(productGuid);

            if (!_prdRepo.DeleteProduct(product))
            {
                ModelState.AddModelError("", $"An error ocurred when deleting the record {product.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
