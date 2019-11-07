using System.Collections.Generic;
using MongoDB.Driver;
using Zebra.Models;

namespace Zebra.Services
{
    public interface IProductRepository
    {
        List<Product> Get();
        Product Get(string id);
        Product Create(Product book);
        void Update(string id, Product bookIn);
        void Remove(Product bookIn);
        void Remove(string id);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _products = database.GetCollection<Product>("Products");
        }

        public List<Product> Get() =>
            _products.Find(book => true).ToList();

        public Product Get(string id) =>
            _products.Find(book => book.Id == id).FirstOrDefault();

        public Product Create(Product book)
        {
            _products.InsertOne(book);
            return book;
        }

        public void Update(string id, Product bookIn) =>
            _products.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Product bookIn) =>
            _products.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _products.DeleteOne(book => book.Id == id);
    }
}