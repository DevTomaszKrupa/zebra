using MongoDB.Driver;
using System.Collections.Generic;
using Zebra.Database.Models;

namespace Zebra.Database.Repository
{
    public interface IShoppingCardProductRepository
    {
        List<ShoppingCardProduct> Get();
        ShoppingCardProduct GetById(string id);
        ShoppingCardProduct GetByProductId(string productId);
        ShoppingCardProduct Create(ShoppingCardProduct item);
        void Update(string id, ShoppingCardProduct item);
        void Remove(ShoppingCardProduct item);
        void Remove(string id);
        void Truncate();
    }

    public class ShoppingCardProductRepository : IShoppingCardProductRepository
    {
        private readonly IMongoCollection<ShoppingCardProduct> _shoppingCardProducts;

        public ShoppingCardProductRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _shoppingCardProducts = database.GetCollection<ShoppingCardProduct>("ShoppingCardProducts");
        }

        public List<ShoppingCardProduct> Get()
            => _shoppingCardProducts.Find(x => true).ToList();

        public ShoppingCardProduct GetById(string id)
            => _shoppingCardProducts.Find(x => x.Id == id).FirstOrDefault();

        public ShoppingCardProduct GetByProductId(string productId)
            => _shoppingCardProducts.Find(x => x.ProductId == productId).FirstOrDefault();

        public ShoppingCardProduct Create(ShoppingCardProduct item)
        {
            _shoppingCardProducts.InsertOne(item);
            return item;
        }

        public void Update(string id, ShoppingCardProduct item) =>
            _shoppingCardProducts.ReplaceOne(x => x.Id == id, item);

        public void Remove(ShoppingCardProduct item) =>
            _shoppingCardProducts.DeleteOne(x => x.Id == item.Id);

        public void Remove(string id) =>
            _shoppingCardProducts.DeleteOne(x => x.Id == id);

        public void Truncate() =>
            _shoppingCardProducts.DeleteMany(x => true);
    }
}