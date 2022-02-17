using TektonProductsAPI.Models;

namespace TektonProductsAPI.Repository.IRepository
{
    public interface IProductsRepository
    {
        ICollection<Product> GetProducts();

        Product GetProduct(Guid productGuid);
        bool ProductExists(string name);
        bool ProductExists(Guid  Guid);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        bool Save();


    }
}
