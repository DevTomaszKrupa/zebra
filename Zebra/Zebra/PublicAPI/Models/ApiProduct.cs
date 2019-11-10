namespace Zebra.PublicAPI.Models
{
    public class ApiProduct
    {
        public ApiProduct(string id, string name, decimal price, string barcode)
        {
            Id = id;
            Name = name;
            Price = price;
            Barcode = barcode;
        }

        public string Id { get; }
        public string Name { get; }
        public string Barcode { get; }
        public decimal Price { get; }
    }
}
