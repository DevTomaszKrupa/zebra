using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Zebra.Database.Models;
using Zebra.Database.Repository;
using Zebra.PandaSystem;
using Zebra.PandaSystem.Models;

namespace Zebra.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IPandaApi _pandaApi;

        public ProductController(IProductRepository productRepository, IPandaApi pandaApi)
        {
            _productRepository = productRepository;
            _pandaApi = pandaApi;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateConfirmed(ProductDto model)
        {
            _productRepository.Create(new Product
            {
                Barcode = model.Barcode,
                Category = model.Category,
                Count = model.Count,
                Description = model.Description,
                Name = model.Name,
                Price = model.Price
            });

            var products = _productRepository.Get().Select(x => new ProductDto
            {
                Name = x.Name,
                Barcode =  x.Barcode,
                Category = x.Category,
                Count = x.Count,
                Description = x.Description,
                Id = x.Id,
                Price = x.Price
            }).ToList();

            var bulkUpdateReq = new BulkUpdateRequest
            {
                Products = products,
                ShopUniqueCode = "ffa_231_dasdk21_213"
            };
            _pandaApi.BulkUpdate(bulkUpdateReq);
            return Redirect("/Home");
        }
    }
}