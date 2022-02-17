using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TektonProductsAPI.Models;
using TektonProductsAPI.Repository.IRepository;
using Xunit;

namespace TektonProductsAPI.Tests.ApiTestBase
{
    public class ProductHelper
    {
        public Product CreteProductModel() 
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = It.Is<string>(x => x.StartsWith("TestName")),
                Description = It.Is<string>(x => x.StartsWith("TestDescription"))
            };
            return product;
        }
        public bool MockCreateProduct(Product product) 
        {
            var productMock = new Mock<IProductsRepository>();
            productMock.Setup(p => p.CreateProduct(product)).Returns(true);

            var result = productMock.Object.CreateProduct(product);
            
            return result;
        }
        public Product MockGetProduct(Guid productGuid) 
        {
            var productMock = new Mock<IProductsRepository>();
            var product = CreteProductModel();

            productMock.Setup(p => p.GetProduct(product.Id)).Returns(product);
            var result = productMock.Object.GetProduct(product.Id);

            return result;
        }
        public ICollection<Product> MockGetProducts() 
        {
            var productMock = new Mock<IProductsRepository>();
            var product = CreteProductModel();
            var productList = new List<Product>();
            productList.Add(product);
            productMock.Setup(p => p.GetProducts()).Returns(productList);

            var result = productMock.Object.GetProducts();

            return result;
        }
        public bool MockDeleteProduct(Product product)
        {
            var productMock = new Mock<IProductsRepository>();

            productMock.Setup(p => p.DeleteProduct(product)).Returns(true);
            var result = productMock.Object.DeleteProduct(product);

            return result;
        }
        public bool MockUpdateProduct(Product product)
        {
            var productMock = new Mock<IProductsRepository>();

            product.Name = $"Testing Update{product.Name}";
            productMock.Setup(p => p.UpdateProduct(product)).Returns(true);
            var result = productMock.Object.UpdateProduct(product);

            return result;
        }
    }
}
