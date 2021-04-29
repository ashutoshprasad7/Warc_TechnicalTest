using ProductShopDataObjects.Dtos;
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
        Task<IEnumerable<IProduct>> GetAllProductsAsync();

        IProduct GetProductById(int? id);
        Task<Result<IProduct>> GetProductByIdAsync(int? id);

        void SaveProduct(IProduct product);
        Task<Result> SaveProductAsync(IProduct product);
    }
}
