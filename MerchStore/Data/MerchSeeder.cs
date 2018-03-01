using MerchStore.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchStore.Data
{
    public class MerchSeeder
    {
        private readonly MerchContext ctx;
        private readonly IHostingEnvironment hosting;
        private readonly UserManager<StoreUser> userManager;

        public MerchSeeder(MerchContext ctx, 
            IHostingEnvironment hosting,
            UserManager<StoreUser> userManager)
        {
            this.ctx = ctx;
            this.hosting = hosting;
            this.userManager = userManager;
        }

        public async Task Seed()
        {
            this.ctx.Database.EnsureCreated();

            var user = await this.userManager.FindByEmailAsync("donal@merchstore.com");

            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Donal",
                    LastName = "Devine",
                    UserName = "donal@merchstore.com",
                    Email = "donal@merchstore.com"
                };

                var result = await this.userManager.CreateAsync(user, "P@ssw0rd!");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default user");
                }

            }

            if (!this.ctx.Products.Any())
            {
                var file = Path.Combine(this.hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(file);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                this.ctx.Products.AddRange(products);

                var order = new Order()
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "12345",
                    User = user,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };

                this.ctx.Orders.Add(order);

                this.ctx.SaveChanges();
            }
        }
    }
}
