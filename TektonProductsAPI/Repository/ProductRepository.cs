using TektonProductsAPI.Data;
using TektonProductsAPI.Models;
using TektonProductsAPI.Repository.IRepository;

namespace TektonProductsAPI.Repository
{
    public class ProductRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool DeleteProduct(Product product)
        {
            _db.Product.Remove(product);
            return Save();
        }

        public bool CreateProduct(Product product)
        {
            _db.Product.Add(product);
            return Save();
        }

        public ICollection<Product> GetProducts()
        {
            return _db.Product.OrderBy(p => p.Name).ToList();
        }

        public Product GetProduct(Guid productGuid)
        {
            return _db.Product.FirstOrDefault(c => c.Id == productGuid);
        }

        public bool ProductExists(string name)
        {
           bool value = _db.Product.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());

            return value; 
        }

        public bool ProductExists(Guid Guid)
        {
            return _db.Product.Any(c => c.Id == Guid);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateProduct(Product product)
        {
            _db.Product.Update(product);
            return Save();
        }
    }
}
