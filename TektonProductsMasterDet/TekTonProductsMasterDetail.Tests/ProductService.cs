using Xunit;
using Moq;
using System.Collections.Generic;

namespace TekTonProductsMasterDetail.Tests
{
    public class ProductService
    {
        private readonly ProductController _productController;
        private Mock<List<Product>> _mockProductList;

        public ProductService() 
        {
            _mockProductList = new Mock<List<Product>>();
            _productController = new ProductController(_productController.Object);
        }

        [Fact]
        public void Test1()
        {
            //arrange
            var mockProducts = new List<Product>
            {
                new Product
            }
        }
    }
}