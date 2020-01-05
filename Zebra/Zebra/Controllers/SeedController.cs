using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Zebra.Database.Models;
using Zebra.Database.Repository;
using Zebra.PandaSystem.Models;

namespace Zebra.Controllers
{
    public class SeedController : Controller
    {
        private readonly IProductRepository _productRepository;

        public SeedController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var seed = ReadJsonFile();
            var products = seed.Products.Select(x => new Product
            {
                Name = x.Name,
                Barcode = x.Barcode,
                Count = x.Count,
                Category = x.Category,
                Description = x.Description,
                Price = x.Price
            }).ToList();
            _productRepository.CreateRange(products);
            return new ObjectResult("OK");
        }

        private SeedData ReadJsonFile()
        {
            using (var r = new StreamReader("SeedProducts.json"))
            {
                var json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<SeedData>(json);
            }
        }
    }
}