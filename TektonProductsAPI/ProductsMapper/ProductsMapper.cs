using AutoMapper;
using TektonProductsAPI.Models;
using TektonProductsAPI.Models.Dtos;

namespace TektonProductsAPI.ProductsMapper
{
    public class ProductsMapper : Profile
    {
        public ProductsMapper()
        {
            CreateMap<Product, ProductsDtos>().ReverseMap();
        }
    }
}
