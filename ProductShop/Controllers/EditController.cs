using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProductShop.Models;
using ProductShopBusinessLayer;
using ProductShopBusinessLayer.Classes;
using ProductShopDataObjects.Classes;

namespace ProductShop.Controllers
{
    public class EditController : Controller
    {
        private readonly IProductProvider _productProvider;

        public EditController()
        {
            _productProvider = new ProductProvider();
        }

        // GET: Edit
        [Route("edit/product/{id}")]
        public ActionResult EditProduct(int id)
        {
            var product = _productProvider.GetProductById(id);

            EditProductViewModel model = new EditProductViewModel
            {
                Price = product.Price,
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                ImagePath = product.ImagePath
            };

            return View("EditProduct", model);
        }

        [HttpPost]
        [Route("edit/product/save")]
        public ActionResult SaveProduct(EditProductViewModel model)
        {
            var product = new ProductItem
            {
                Id = model.Id,
                Price = model.Price,
                Title = model.Title,
                Description = model.Description,
                ImagePath = model.ImagePath
            };

            _productProvider.SaveProduct(product);

            return new RedirectResult($"/product/{model.Id}");
        }
    }
}