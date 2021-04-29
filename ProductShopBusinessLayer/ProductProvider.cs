using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductShopBusinessLayer.Classes;
using ProductShopDataLayer;
using ProductShopDataObjects.Classes;

namespace ProductShopBusinessLayer
{
    public class ProductProvider : IProductProvider
    {
        public List<IProduct> GetAllProducts()
        {
            using (ProductShopDataModel productsDb = new ProductShopDataModel())
            {
                List<IProduct> products = new List<IProduct>(productsDb.Products.Select(p => new ProductItem
                {
                    Id = p.Id,
                    Price = p.Price,
                    Title = p.Title,
                    Description = p.Description,
                    ImagePath = p.ImagePath
                }).ToList());

                return products;
            }
        }

        public IProduct GetProductById(int? id)
        {
            using (ProductShopDataModel productsDb = new ProductShopDataModel())
            {
                var dataProduct = productsDb.Products.FirstOrDefault(i => i.Id == id);

                if (dataProduct == null)
                {
                    throw new NullReferenceException($"No product for id {id}");
                }

                IProduct product = new ProductItem
                {
                    Id = dataProduct.Id,
                    Price = dataProduct.Price,
                    Title = dataProduct.Title,
                    Description = dataProduct.Description,
                    ImagePath = dataProduct.ImagePath
                };

                return product;
            }
        }

        public void SaveProduct(IProduct product)
        {
            using (ProductShopDataModel productsDb = new ProductShopDataModel())
            {
                var dataProduct = productsDb.Products.FirstOrDefault(i => i.Id == product.Id);

                if (dataProduct == null)
                {
                    throw new NullReferenceException($"No product for id {product.Id}");
                }

                dataProduct.ImagePath = product.ImagePath;
                dataProduct.Price = product.Price;
                dataProduct.Title = product.Title;
                dataProduct.Description = product.Description;

                productsDb.SaveChanges();
            }
        }
    }
}
