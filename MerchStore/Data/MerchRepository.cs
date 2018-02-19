using MerchStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchStore.Data
{
    public class MerchRepository
    {
        private readonly MerchContext ctx;

        public MerchRepository(MerchContext ctx)
        {
            this.ctx = ctx;
        }

        public IEnumerable<Product> GetAllProducts()
        {

            return this.ctx.Products.OrderBy(p => p.Title).ToList();
        }

        public IEnumerable<Product> GetProductByCategory(string category)
        {

            return this.ctx.Products.OrderBy(p => p.Category == category).ToList();
        }
    }
}
