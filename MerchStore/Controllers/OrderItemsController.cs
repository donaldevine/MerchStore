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
    [Route("/api/orders/{orderid}/items")]
    public class OrderItemsController : Controller
    {
        private readonly IMerchRepository repository;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public OrderItemsController(IMerchRepository repository, 
            ILogger logger, 
            IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = this.repository.GetOrderById(orderId);
            if (order != null) { 
                return Ok(this.mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
            }
            else
            {
                return NotFound("No items in this order.");
            }

        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var order = this.repository.GetOrderById(orderId);
            if (order != null)
            {
                var item = order.Items.Where(i => i.Id == id).FirstOrDefault();

                if (item != null)
                {
                    return Ok(this.mapper.Map<OrderItem, OrderItemViewModel>(item));
                }
            }

            return NotFound("No items in this order.");            
        }
    }
}
