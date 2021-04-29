using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductShop.Models;
using ProductShopBusinessLayer;
using ProductShopDataObjects.Classes;

namespace ProductShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductProvider _productProvider;

        public ProductController()
        {
            _productProvider = new ProductProvider();
        }
        public ActionResult Index()
        {
            return View();
        }

        [Route("product/{id}")]
        public ActionResult Product(int? id)
        {
            ProductViewModel model = new ProductViewModel
            {
                Product = _productProvider.GetProductById(id)
            };

            return View("Product", model);
        }
    }
}