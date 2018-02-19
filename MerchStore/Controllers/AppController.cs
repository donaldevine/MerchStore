using MerchStore.Data;
using MerchStore.Services;
using MerchStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerchStore.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService mailService;
        private readonly IMerchRepository repository;

        public AppController(IMailService mailService, IMerchRepository repository)
        {
            this.mailService = mailService;
            this.repository = repository;            
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Send the email    
                this.mailService.SendMessage("test@testing.test", model.Subject, model.Message);                
                ModelState.Clear();
            }

            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About Us";

            return View();
        }

        public IActionResult Shop()
        {
            var results = this.repository.GetAllProducts();                

            return View(results);
        }
    }
}
