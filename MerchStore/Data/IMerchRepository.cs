using System.Collections.Generic;
using MerchStore.Data.Entities;

namespace MerchStore.Data
{
    public interface IMerchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductByCategory(string category);

        
        bool SaveAll();
        IEnumerable<Order> GetAllOrders(bool includeItems);

        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);

        Order GetOrderById(int id);
        void AddEntity(object model);
        
    }
}