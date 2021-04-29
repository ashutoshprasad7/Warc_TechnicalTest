using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductShop.Controllers.Api;
using ProductShop.Models;
using ProductShopBusinessLayer.Classes;
using ProductShopDataObjects.Classes;
using ProductShopDataObjects.Dtos;

namespace ProductShop.Tests
{
    [TestClass]
    public class ProductApiControllerTests
    {
        [TestMethod]
        public void Test_GetProducts_Returns_AllProducts_Correctly()
        {
            var testProductList = new List<IProduct>()
            {
                new ProductItem { Id = 1, Title = "Title 1", Description = "Desc 1", ImagePath = "Path 1", Price = 100 },
                new ProductItem { Id = 2, Title = "Title 2", Description = "Desc 2", ImagePath = "Path 2", Price = 200 },
            };

            var mock = new Mock<IProductProvider>();
            mock.Setup(x => x.GetAllProductsAsync())
                .Returns(Task.FromResult(testProductList.AsEnumerable()));

            var productApi = new ProductApiController(mock.Object);
            var productListResponse = productApi.GetProducts().GetAwaiter().GetResult();

            productListResponse.Should().BeEquivalentTo(testProductList);
        }

        [TestMethod]
        public void Test_GetProduct_Returns_Matching_Product()
        {
            var testProductList = new List<IProduct>()
            {
                new ProductItem { Id = 1, Title = "Title 1", Description = "Desc 1", ImagePath = "Path 1", Price = 100 },
                new ProductItem { Id = 2, Title = "Title 2", Description = "Desc 2", ImagePath = "Path 2", Price = 200 },
            };

            var mock = new Mock<IProductProvider>();            
            mock.Setup(x => x.GetProductByIdAsync(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    var matchedPrd = testProductList.FirstOrDefault(x => x.Id == id);
                    var found = matchedPrd != null;
                    var errMsg = found ? new string[0] : new[] { "No product found" };
                    var matchedPrdRes = new Result<IProduct>(found, matchedPrd, errMsg);
                    return Task.FromResult(matchedPrdRes);
                });

            var productApi = new ProductApiController(mock.Object);

            var testProductId = 2;
            var productResponse = productApi.GetProduct(testProductId).GetAwaiter().GetResult();

            var expected = testProductList.FirstOrDefault(x => x.Id == testProductId);
            productResponse.Should().Equals(expected);
        }

        [TestMethod]
        public void Test_SaveProduct_Updates_Product_Correctly()
        {
            var testProductList = new List<IProduct>()
            {
                new ProductItem { Id = 1, Title = "Title 1", Description = "Desc 1", ImagePath = "Path 1", Price = 100 },
                new ProductItem { Id = 2, Title = "Title 2", Description = "Desc 2", ImagePath = "Path 2", Price = 600 },
            };

            var mock = new Mock<IProductProvider>();

            mock.Setup(x => x.GetProductByIdAsync(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    var matchedPrd = testProductList.FirstOrDefault(x => x.Id == id);
                    var found = matchedPrd != null;
                    var errMsg = found ? new string[0] : new[] { "No product found" };
                    var matchedPrdRes = new Result<IProduct>(found, matchedPrd, errMsg);
                    return Task.FromResult(matchedPrdRes);
                });

            mock.Setup(x => x.SaveProductAsync(It.IsAny<IProduct>()))
                .Returns((IProduct dto) =>
                {
                    var matchedPrd = testProductList.FirstOrDefault(x => x.Id == dto.Id);
                    var found = matchedPrd != null;
                    var errMsg = found ? new string[0] : new[] { "No product found" };

                    if(found)
                    {
                        matchedPrd.Price = dto.Price;
                        matchedPrd.Title = dto.Title;
                        matchedPrd.Description = dto.Description;
                        matchedPrd.ImagePath = dto.ImagePath;
                    }

                    var matchedPrdRes = new Result(found, errMsg);
                    return Task.FromResult(matchedPrdRes);
                });

            var productApi = new ProductApiController(mock.Object);

            var testSaveProduct = new ProductItemModel
            {
                Id = 2,
                Title = "New Title 2",
                Description = "New Description 2",
                ImagePath = "New image path 2",
                Price = 5000
            };

            var productResponse = productApi.SaveProduct(testSaveProduct).GetAwaiter().GetResult();

            productResponse.Should().NotBeNull();
            productResponse.Success.Should().BeTrue();
        }
    }
}
