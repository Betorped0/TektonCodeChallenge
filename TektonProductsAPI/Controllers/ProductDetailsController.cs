using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TektonProductsAPI.Models;
using TektonProductsAPI.Models.Dtos;
using TektonProductsAPI.Repository.IRepository;

namespace TektonProductsAPI.Controllers
{
    [Route("api/product-details")]
    [ApiController]
    public class ProductDetailsController : Controller
    {
        private readonly IProductDetailRepository _prdDetRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        public ProductDetailsController(IProductDetailRepository prdDetRepo, IMapper mapper)
        {
            _prdDetRepo = prdDetRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProductDetails()
        {
            var productDetailsList = _prdDetRepo.GetProductDetails();

            var productDetailsListDto = new List<ProductDetailDto>();

            foreach (var product in productDetailsList)
            {
                productDetailsListDto.Add(_mapper.Map<ProductDetailDto>(product));
            }

            return Ok(productDetailsListDto);
        }

        [HttpGet("{productGuid:Guid}", Name = "GetProductDetails")]
        public IActionResult GetProductDetails(Guid productGuid)
        {
            var singleProduct = _prdDetRepo.GetProductDetail(productGuid);
            if (singleProduct == null)
            {
                return NotFound();
            }

            var singleProductDetailDto = _mapper.Map<ProductDetailDto>(singleProduct);
            return Ok(singleProductDetailDto);
        }

        [HttpPost]
        public IActionResult CreateProductDetails([FromForm] CreateProductDetail productDetailsDto)
        {
            if (productDetailsDto == null)
            {
                return BadRequest(ModelState);
            }
            var file = productDetailsDto.Image;
            var mainPath = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var imageName = productDetailsDto.productGuid.ToString();
            var uploadedPath = Path.Combine();
            if (_prdDetRepo.ProductDetailExists(productDetailsDto.DetailedDescription))
            {
                ModelState.AddModelError("", "Product already exists.");
                return StatusCode(404, ModelState);
            }
            var productDet = _mapper.Map<ProductDetail>(productDetailsDto);

            if (!_prdDetRepo.CreateProductDetail(productDet))
            {
                ModelState.AddModelError("", $"An error occured when saving productDetail {productDet.Id}");
                return StatusCode(500, ModelState);
            }
            return Ok(productDet);
        }

        [HttpPatch("{productGuid:Guid}", Name = "UpdateProductetails")]
        public IActionResult UpdateProductDetails(Guid productGuid, [FromBody]ProductDetailDto productDetailDto) 
        {
            if (productDetailDto == null || productGuid != productDetailDto.Id) 
            {
                return BadRequest(ModelState);
            }
            var productDetail = _mapper.Map<ProductDetail>(productDetailDto);
            if (!_prdDetRepo.UpdateProductDetail(productDetail)) 
            {
                ModelState.AddModelError("", $"Error ocurred while updating record {productDetail.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{productGuid:Guid}", Name = "DeleteProductDetails")]
        public IActionResult DeleteProductDetail(Guid productGuid)
        {
            if (!_prdDetRepo.ProductDetailExists(productGuid))
            {
                return NotFound();
            }

            var product = _prdDetRepo.GetProductDetail(productGuid);

            if (!_prdDetRepo.DeleteProductDetail(product))
            {
                ModelState.AddModelError("", $"An error ocurred when deleting the record {product.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
