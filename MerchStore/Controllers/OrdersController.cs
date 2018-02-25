using AutoMapper;
using MerchStore.Data;
using MerchStore.Data.Entities;
using MerchStore.ViewModels;
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
        private readonly IMapper mapper;

        public OrdersController(IMerchRepository repository, 
            ILogger<OrdersController> logger, 
            IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }   

        [HttpPost()]
        public IActionResult Post([FromBody]OrderViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var newOrder = this.mapper.Map<OrderViewModel, Order>(model);

                    if (this.repository.SaveAll())
                    {                                                
                        return Created($"/api/orders/{newOrder.Id}", this.mapper.Map<Order, OrderViewModel>(newOrder));
                    }                    
                }
                
                return BadRequest(ModelState);
                
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Failed to save a new order: {ex}");
                throw;
            }
            
        }

        [HttpGet()]
        public IActionResult Get()
        {
            try
            {
                return Ok(this.mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(this.repository.GetAllOrders()));
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
                    return Ok(this.mapper.Map<Order, OrderViewModel>(order));
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
