using System.Collections.Generic;

namespace Zebra.PandaSystem.Models
{
    public class BulkUpdateRequest
    {
        public string ShopUniqueCode { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
