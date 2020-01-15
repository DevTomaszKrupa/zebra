using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zebra.Database.Models;
using Zebra.Database.Repository;
using Zebra.PandaSystem.Models;
using Flurl.Http;

namespace Zebra.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly string _pandaUrl;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _pandaUrl = "http://panda-api/api";
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateConfirmed(ProductDto model)
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
                Barcode = x.Barcode,
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

            var bulkUpdateUrl = _pandaUrl + "/product";

            await bulkUpdateUrl.PostJsonAsync(bulkUpdateReq);

            return Redirect("/Home");
        }
    }
}