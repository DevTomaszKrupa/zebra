using Microsoft.AspNetCore.Mvc;
using Zebra.Database.Models;
using Zebra.Database.Repository;

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

        public IActionResult ProductList()
        {
            return View();
        }
    }
}
