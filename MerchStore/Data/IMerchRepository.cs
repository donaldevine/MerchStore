using System.Collections.Generic;
using MerchStore.Data.Entities;

namespace MerchStore.Data
{
    public interface IMerchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductByCategory(string category);

        bool SaveAll();
    }
}