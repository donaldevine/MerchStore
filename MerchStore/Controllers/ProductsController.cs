using MerchStore.Data;
using MerchStore.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchStore.Controllers
{
    [Route("api/[Controller]")]
    public class ProductsController : Controller 
    {
        private readonly IMerchRepository repository;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(IMerchRepository repository,
            ILogger<ProductsController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(this.repository.GetAllProducts());
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Failed to get products. {ex}");
                return BadRequest("Failed to get proudcts.");
            }            
        }
    }
}
