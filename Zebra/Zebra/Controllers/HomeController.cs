using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Zebra.Database.Models;
using Zebra.Database.Repository;
using Zebra.Models;
using Zebra.ViewModels;

namespace Zebra.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCardProductRepository _shoppingCardProductRepository;

        public HomeController(IProductRepository productRepository, IShoppingCardProductRepository shoppingCardProductRepository)
        {
            _productRepository = productRepository;
            _shoppingCardProductRepository = shoppingCardProductRepository;
        }

        public IActionResult Index()
        {
            //_productRepository.Create(new Product
            //{ Author = "asd", Category = "asdasdas", Description = 1, Name = "name" });
            //var test = _productRepository.Get();
            return View();
        }

        public IActionResult ProductList(string searchPhrase)
        {
            var databaseProducts = _productRepository.Get(searchPhrase);

            var viewModels = databaseProducts
                .Select(x => new ProductVM(x.Id, x.Name, x.Description))
                .ToArray();

            return PartialView(viewModels);
        }

        public IActionResult ShoppingCard()
        {
            var items = _shoppingCardProductRepository.Get();
            var vmItems = items.Select(x => new ShoppingCardProductVM(x.Id, x.Name, x.Count)).ToList();
            var vm = new ShoppingCardVM(vmItems);
            return PartialView(vm);
        }

        public IActionResult AddToShoppingCard(int count, string productId)
        {
            var entity = _productRepository.GetById(productId);
            var product = _shoppingCardProductRepository.GetByProductId(productId);

            if (product == null) AddShoppingCardProduct(entity.Name, count, productId);
            else UpdateShoppingCardProduct(product, count);

            var items = _shoppingCardProductRepository.Get();
            var vmItems = items.Select(x => new ShoppingCardProductVM(x.Id, x.Name, x.Count)).ToList();
            var vm = new ShoppingCardVM(vmItems);
            return PartialView("ShoppingCard", vm);
        }

        private void UpdateShoppingCardProduct(ShoppingCardProduct product, int count)
        {
            product.Count += count;
            _shoppingCardProductRepository.Update(product.Id, product);
        }

        private void AddShoppingCardProduct(string name, int count, string productId)
        {
            _shoppingCardProductRepository.Create(new ShoppingCardProduct
            {
                Count = count,
                Name = name,
                ProductId = productId
            });
        }
    }
}
