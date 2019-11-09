namespace Zebra.ViewModels
{
    public class ShoppingCardProductVM
    {
        public ShoppingCardProductVM(string id, string name, int count)
        {
            Id = id;
            Name = name;
            Count = count;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }
    }
}