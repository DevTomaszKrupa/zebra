using Microsoft.AspNetCore.Mvc;
using Zebra.Database.Repository;

namespace Zebra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCardController : ControllerBase
    {
        private readonly IShoppingCardProductRepository _shoppingCardProductRepository;

        public ShoppingCardController(IShoppingCardProductRepository shoppingCardProductRepository)
        {
            _shoppingCardProductRepository = shoppingCardProductRepository;
        }

        [HttpPost("delete")]
        public IActionResult Delete()
        {
            _shoppingCardProductRepository.Truncate();
            return Ok();
        }
    }
}