using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductShopDataObjects.Classes
{
    public interface IProduct
    {
        int Id { get; set; }
        decimal Price { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string ImagePath { get; set; }
    }
}
