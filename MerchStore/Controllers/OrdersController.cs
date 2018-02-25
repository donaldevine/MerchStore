using MerchStore.Data;
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
    public class OrdersController : Controller
    {
        private readonly IMerchRepository repository;
        private readonly ILogger<OrdersController> logger;

        public OrdersController(IMerchRepository repository, ILogger<OrdersController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }   

        [HttpGet()]
        public IActionResult Get()
        {
            try
            {
                return Ok(this.repository.GetAllOrders());
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = this.repository.GetOrderById(id);

                if (order != null)
                {
                    return Ok(order);
                }
                else
                {
                    return NotFound();
                }                
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }

    }
}
