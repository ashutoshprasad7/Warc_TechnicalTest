using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductShopDataObjects.Classes
{
    public interface IProductProvider
    {
        List<IProduct> GetAllProducts();

        IProduct GetProductById(int? id);

        void SaveProduct(IProduct product);
    }
}
