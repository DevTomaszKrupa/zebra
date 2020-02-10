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
        Product Create(Product product);
        void CreateRange(List<Product> products);
        void Truncate();
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

        public void CreateRange(List<Product> products)
            => _products.InsertMany(products);
        
        public void Truncate() =>
            _products.DeleteMany(x => true);
    }
}