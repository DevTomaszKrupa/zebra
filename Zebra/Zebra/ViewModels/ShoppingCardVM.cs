using System.Collections.Generic;

namespace Zebra.ViewModels
{
    public class ShoppingCardVM
    {
        public ShoppingCardVM(List<ShoppingCardProductVM> products)
        {
            Products = products;
        }

        public List<ShoppingCardProductVM> Products { get; set; }
    }
}