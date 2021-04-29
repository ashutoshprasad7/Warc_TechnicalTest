using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductShop.Models
{
    public class EditProductViewModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}