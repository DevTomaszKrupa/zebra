using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Zebra.Database.Repository;
using Zebra.PublicAPI.Models;

namespace Zebra.PublicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var dbItems = _productRepository.Get();
            var products = dbItems
                .Select(x => new ApiProduct(x.Id, x.Name, 10, x.Barcode))
                .ToList(); // TODO Price
            return Ok(products);
        }
    }
}