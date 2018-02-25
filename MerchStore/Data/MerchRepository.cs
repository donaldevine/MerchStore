using MerchStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchStore.Data
{
    public class MerchRepository : IMerchRepository
    {
        private readonly MerchContext ctx;
        private readonly ILogger<MerchRepository> logger;

        public MerchRepository(MerchContext ctx, ILogger<MerchRepository> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }

        public void AddEntity(object model)
        {
            this.ctx.Add(model);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return this.ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToList();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                this.logger.LogInformation("Get All Products");
                return this.ctx.Products.OrderBy(p => p.Title).ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public Order GetOrderById(int id)
        {
            return this.ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductByCategory(string category)
        {
            return this.ctx.Products.OrderBy(p => p.Category == category).ToList();
        }

        public bool SaveAll()
        {
            return this.ctx.SaveChanges() > 0;
        }
    }
}
