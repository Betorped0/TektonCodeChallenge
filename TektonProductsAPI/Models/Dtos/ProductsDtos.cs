using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TektonProductsAPI.Models.Dtos
{
    public class ProductsDtos
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Name field is mandatory")]
        public String Name { get; set; }
        public string? Description { get; set; }        
    }
}
