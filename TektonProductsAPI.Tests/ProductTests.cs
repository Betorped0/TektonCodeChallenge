using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using TektonProductsAPI.Controllers;
using TektonProductsAPI.Models;
using TektonProductsAPI.Repository.IRepository;
using TektonProductsAPI.Tests.ApiTestBase;
using Xunit;

namespace TektonProductsAPI.Tests
{
    public class ProductTests
    {
        ProductHelper prdHlpr = new ProductHelper();

        [Fact]
        public void CreateProducts_ExistingProducts_ShouldReturnTrue()
        {
            //arrange
            var product = prdHlpr.CreteProductModel();

            //act
            var result = prdHlpr.MockCreateProduct(product);

            //assert
            Assert.True(result);
        }
        [Fact]
        public void GetProducts_ExistingProducts_ShouldReturnProduct()
        {
            //arrange
            var product = prdHlpr.CreteProductModel();

            //act
            var result = prdHlpr.MockGetProduct(product.Id);

            //assert
            Assert.IsType<Product>(result);
        }

        [Fact]
        public void GetProducts_ExistingProducts_ShouldReturnListOfProducts()
        {
            //arrange
            //act
            var result = prdHlpr.MockGetProducts();

            //assert
            Assert.IsType<List<Product>>(result);
        }
        [Fact]
        public void DeleteProduct_ExistingProduct_ShouldReturnProduct()
        {
            //arrange
            var product = prdHlpr.CreteProductModel();

            //act
            var result = prdHlpr.MockDeleteProduct(product);

            //assert
            Assert.True(result);
        }
    }
}