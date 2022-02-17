using System.ComponentModel.DataAnnotations;
using static TektonProductsAPI.Models.ProductDetail;

namespace TektonProductsAPI.Models.Dtos
{
    public class CreateProductDetail
    {
        public string DetailedDescription { get; set; }
        public string Brand { get; set; }
        [Required(ErrorMessage = "Quantity field is mandatory")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "ImagePath field is mandatory")]
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "Price field is mandatory")]
        public float? Price { get; set; }
        public RetailCode Code { get; set; }
        [Required(ErrorMessage = "ProductGuid field is mandatory")]
        public Guid productGuid { get; set; }
    }
}
