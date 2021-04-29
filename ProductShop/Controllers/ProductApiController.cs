using ProductShop.Models;
using ProductShopBusinessLayer;
using ProductShopBusinessLayer.Classes;
using ProductShopDataObjects.Classes;
using ProductShopDataObjects.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProductShop.Controllers.Api
{
    [RoutePrefix("api/product")]
    public class ProductApiController : ApiController
    {
        private readonly IProductProvider _productProvider;

        public ProductApiController()
        {
            _productProvider = new ProductProvider();
        }

        public ProductApiController(IProductProvider productProvider)
        {
            _productProvider = productProvider;
        }

        [HttpGet]
        [Route("all")]
        public async Task<List<ProductItemModel>> GetProducts()
        {
            var products = await _productProvider.GetAllProductsAsync();
            var res = products.Select(x => new ProductItemModel
            {
                Id = x.Id,
                Description = x.Description,
                ImagePath = x.ImagePath,
                Price = x.Price,
                Title = x.Title

            }).ToList();

            return res;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Result<ProductItemModel>> GetProduct(int id)
        {
            var getProductResult = await _productProvider.GetProductByIdAsync(id);
            if (getProductResult.Success)
            {
                var product = getProductResult.Data;
                var dto = new ProductItemModel
                {
                    Id = product.Id,
                    Description = product.Description,
                    ImagePath = product.ImagePath,
                    Price = product.Price,
                    Title = product.Title
                };

                return new Result<ProductItemModel>(true, dto);
            }

            return new Result<ProductItemModel>(false, null, getProductResult.Errors);
        }

        [HttpPost]
        [Route("save")]
        public async Task<Result> SaveProduct([FromBody] ProductItemModel dto)
        {
            if (dto == null)
            {
                return new Result(false, new[] { "Model binding failed", $"Input parameter {nameof(dto)} is null" });
            }

            var product = await _productProvider.GetProductByIdAsync(dto.Id);
            if (product == null)
            {
                return new Result(false, new[] { $"No product found with Id : { dto.Id }" });
            }

            var prodToSave = new ProductItem
            {
                Id = dto.Id,
                Description = dto.Description,
                ImagePath = dto.ImagePath,
                Price = dto.Price,
                Title = dto.Title
            };

            var saveRes = await _productProvider.SaveProductAsync(prodToSave);
            return saveRes;
        }
    }
}