using TektonProductsAPI.Models;

namespace TektonProductsAPI.Repository.IRepository
{
    public interface IProductDetailRepository
    {
        ICollection<ProductDetail> GetProductDetails();
        ICollection<ProductDetail> GetDetailsOfProducts(Guid productGuid);
        ProductDetail GetProductDetail(Guid productDetailGuid);
        bool ProductDetailExists(string name);
        bool ProductDetailExists(Guid  Guid);
        IEnumerable<ProductDetail> FilterProductDetail(string description);
        bool CreateProductDetail(ProductDetail productDetail);
        bool UpdateProductDetail(ProductDetail ProductDetail);
        bool DeleteProductDetail(ProductDetail ProductDetail);
        bool Save();
    }
}
