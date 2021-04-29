using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductShopDataObjects.Classes;

namespace ProductShopBusinessLayer.Classes
{
    public class ProductItem : IProduct
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}
