using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using Zebra.Database.Models;

namespace Zebra.Database.Repository
{
    public interface IProductRepository
    {
        List<Product> Get(string searchPhrase = "");
        Product GetById(string id);
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

        public List<Product> Get(string searchPhrase = "")
        {
            if (string.IsNullOrEmpty(searchPhrase))
                return _products.Find(x => true).ToList();

            return _products
                    .Find(x => true)
                    .ToList()
                    .Where(x => x.Name.Contains(searchPhrase, StringComparison.InvariantCultureIgnoreCase))
                    .ToList();
        }

        public Product GetById(string id) =>
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