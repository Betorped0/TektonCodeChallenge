using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TektonProductsAPI.Models
{
    public class ProductDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string DetailedDescription { get; set; }
        public string Brand { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string ImagePath { get; set; }
        [Required]
        public float? Price { get; set; }
        public enum RetailCode { Online, Ctalog, OnSite, WholeSale }
        public RetailCode Code { get; set; }
        public DateTime CreationDate { get; set; }
        [Required]
        public Guid productGuid { get; set; }
        [ForeignKey("productGuid")]
        public Product Product { get; set; }

    }
}
