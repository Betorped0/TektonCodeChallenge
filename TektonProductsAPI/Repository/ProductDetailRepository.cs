using Microsoft.EntityFrameworkCore;
using TektonProductsAPI.Data;
using TektonProductsAPI.Models;
using TektonProductsAPI.Repository.IRepository;

namespace TektonProductsAPI.Repository
{
    public class ProductDetailRepository : IProductDetailRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductDetailRepository(ApplicationDbContext db) 
        {
            _db = db;
        }
        public bool CreateProductDetail(ProductDetail productDetail)
        {
            _db.ProductDetail.Add(productDetail);
            return Save();
        }

        public bool DeleteProductDetail(ProductDetail productDetail)
        {
            _db.ProductDetail.Remove(productDetail);
            return Save();
        }

        public IEnumerable<ProductDetail> FilterProductDetail(string description)
        {
            IQueryable<ProductDetail> query = _db.ProductDetail;

            if (string.IsNullOrEmpty(description)) 
            {
                query = query.Where(e => e.DetailedDescription.Contains(description));
            }
            return query.ToList();
        }

        public ICollection<ProductDetail> GetDetailsOfProducts(Guid productGuid)
        {
            return _db.ProductDetail.Include(p => p.Product).Where(p => p.Id == productGuid).ToList();
        }

        public ProductDetail GetProductDetail(Guid productDetailGuid)
        {
            return _db.ProductDetail.FirstOrDefault(pd => pd.Id == productDetailGuid);
        }

        public ICollection<ProductDetail> GetProductDetails()
        {
            return _db.ProductDetail.OrderBy(p => p.DetailedDescription).ToList();
        }

        public bool ProductDetailExists(string name)
        {
            bool value = _db.Product.Any(p => p.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool ProductDetailExists(Guid productGuid)
        {
            return _db.ProductDetail.Any(p => p.productGuid == productGuid);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateProductDetail(ProductDetail productDetail)
        {
            _db.ProductDetail.Update(productDetail);
            return Save();
        }
    }
}
