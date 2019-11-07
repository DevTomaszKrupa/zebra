using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Zebra.Models;
using Zebra.Services;
using Pigoen.Models;

namespace Zebra.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            _productRepository.Create(new Product
                {Author = "asd", Category = "asdasdas", Description = 1, Name = "name"});
            var test  = _productRepository.Get();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
